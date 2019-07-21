using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xacor.Collections;

namespace Xacor.Game.ECS
{
    public sealed class EntityManager
    {
        private readonly Bag<Bag<IComponent>> _componentsByType;

        private readonly Bag<Entity> _removedAndAvailable;

        private readonly HashSet<Tuple<Entity, ComponentType>> _componentsToBeRemoved = new HashSet<Tuple<Entity, ComponentType>>();

        private readonly Dictionary<long, Entity> _uniqueIdToEntities;

        private readonly EntityWorld _entityWorld;

        private int _nextAvailableId;

        private readonly Bag<int> _identifierPool;

        public EntityManager(EntityWorld entityWorld)
        {
            Debug.Assert(entityWorld != null, "EntityWorld must not be null.");

            _uniqueIdToEntities = new Dictionary<long, Entity>();
            _removedAndAvailable = new Bag<Entity>();
            _componentsByType = new Bag<Bag<IComponent>>();
            ActiveEntities = new Bag<Entity>();
            _identifierPool = new Bag<int>(4);
            RemovedEntitiesRetention = 100;
            this._entityWorld = entityWorld;
            RemovedComponentEvent += this.EntityManagerRemovedComponentEvent;
        }

        public event AddedComponentHandler AddedComponentEvent;

        public event AddedEntityHandler AddedEntityEvent;

        public event RemovedComponentHandler RemovedComponentEvent;

        public event RemovedEntityHandler RemovedEntityEvent;

        public Bag<Entity> ActiveEntities { get; private set; }

#if DEBUG
        public int EntitiesRequestedCount { get; private set; }
#endif
        public int RemovedEntitiesRetention { get; set; }
#if DEBUG
        public long TotalCreated { get; private set; }

        public long TotalRemoved { get; private set; }
#endif

        public Entity Create(long? uniqueid = null)
        {
            var id = uniqueid ?? BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);

            var result = _removedAndAvailable.RemoveLast();
            if (result == null)
            {
                var entityId = _identifierPool.IsEmpty
                    ? _nextAvailableId++
                    : _identifierPool.RemoveLast();

                result = new Entity(_entityWorld, entityId);
            }
            else
            {
                result.Reset();
            }

            result.UniqueId = id;
            _uniqueIdToEntities[result.UniqueId] = result;
            ActiveEntities.Set(result.Id, result);
#if DEBUG
            ++EntitiesRequestedCount;

            if (TotalCreated < long.MaxValue)
            {
                ++TotalCreated;
            }
#endif
            AddedEntityEvent?.Invoke(result);

            return result;
        }

        public Bag<IComponent> GetComponents(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            //Debug.Assert(entity.entityManager == this, "");  // TODO

            var entityComponents = new Bag<IComponent>();
            var entityId = entity.Id;
            for (int index = 0, b = _componentsByType.Count; b > index; ++index)
            {
                var components = _componentsByType.Get(index);
                if (components != null && entityId < components.Count)
                {
                    var component = components.Get(entityId);
                    if (component != null)
                    {
                        entityComponents.Add(component);
                    }
                }
            }

            return entityComponents;
        }

        public Bag<Entity> GetEntities(Aspect aspect)
        {
            var entitiesBag = new Bag<Entity>();
            for (int index = 0; index < ActiveEntities.Count; ++index)
            {
                var entity = ActiveEntities.Get(index);
                if (entity != null && aspect.Interests(entity))
                {
                    entitiesBag.Add(entity);
                }
            }

            return entitiesBag;
        }

        public Entity GetEntity(int entityId)
        {
            Debug.Assert(entityId >= 0, "Id must be at least 0.");

            return ActiveEntities.Get(entityId);
        }

        public Entity GetEntityByUniqueId(long entityUniqueId)
        {
            Debug.Assert(entityUniqueId != -1, "Id must != -1");
            _uniqueIdToEntities.TryGetValue(entityUniqueId, out var entity);
            return entity;
        }

        public bool IsActive(int entityId)
        {
            return ActiveEntities.Get(entityId) != null;
        }

        public void Remove(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            //Debug.Assert(entity.entityManager == this, "");  // TODO

            ActiveEntities.Set(entity.Id, null);
            RemoveComponentsOfEntity(entity);
#if DEBUG
            --EntitiesRequestedCount;

            if (TotalRemoved < long.MaxValue)
            {
                ++TotalRemoved;
            }
#endif
            if (_removedAndAvailable.Count < RemovedEntitiesRetention)
            {
                _removedAndAvailable.Add(entity);
            }
            else
            {
                _identifierPool.Add(entity.Id);
            }

            RemovedEntityEvent?.Invoke(entity);

            _uniqueIdToEntities.Remove(entity.UniqueId);
        }

        internal void AddComponent(Entity entity, IComponent component)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            Debug.Assert(component != null, "Component must not be null.");

            var type = ComponentTypeManager.GetTypeFor(component.GetType());

            AddComponent(entity, component, type);
        }

        internal void AddComponent<T>(Entity entity, IComponent component) where T : IComponent
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            Debug.Assert(component != null, "Component must not be null.");

            var type = ComponentTypeManager.GetTypeFor<T>();

            AddComponent(entity, component, type);
        }

        internal void AddComponent(Entity entity, IComponent component, ComponentType type)
        {
            if (type.Id >= _componentsByType.Capacity)
            {
                _componentsByType.Set(type.Id, null);
            }

            var components = _componentsByType.Get(type.Id);
            if (components == null)
            {
                components = new Bag<IComponent>();
                _componentsByType.Set(type.Id, components);
            }

            components.Set(entity.Id, component);

            entity.AddTypeBit(type.Bit);
            AddedComponentEvent?.Invoke(entity, component);

            _entityWorld.RefreshEntity(entity);
        }

        internal IComponent GetComponent(Entity entity, ComponentType componentType)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            Debug.Assert(componentType != null, "Component type must not be null.");

            if (componentType.Id >= _componentsByType.Capacity)
            {
                return null;
            }

            var entityId = entity.Id;
            var bag = _componentsByType.Get(componentType.Id);

            if (bag != null && entityId < bag.Capacity)
            {
                return bag.Get(entityId);
            }

            return null;
        }

        internal void Refresh(Entity entity)
        {
            var systemManager = _entityWorld.SystemManager;
            var systems = systemManager.Systems;
            for (int index = 0, s = systems.Count; s > index; ++index)
            {
                systems.Get(index).OnChange(entity);
            }
        }

        internal void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            RemoveComponent(entity, ComponentType<T>.CType);
        }

        internal void RemoveComponent(Entity entity, ComponentType componentType)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            Debug.Assert(componentType != null, "Component type must not be null.");

            var pair = new Tuple<Entity, ComponentType>(entity, componentType);
            _componentsToBeRemoved.Add(pair);
        }

        internal void RemoveMarkedComponents()
        {
            foreach (var (entity, componentType) in _componentsToBeRemoved)
            {
                var entityId = entity.Id;
                var components = _componentsByType.Get(componentType.Id);

                if (components != null && entityId < components.Count)
                {
                    var componentToBeRemoved = components.Get(entityId);
                    if (RemovedComponentEvent != null && componentToBeRemoved != null)
                    {
                        RemovedComponentEvent(entity, componentToBeRemoved);
                    }

                    entity.RemoveTypeBit(componentType.Bit);
                    _entityWorld.RefreshEntity(entity);
                    components.Set(entityId, null);
                }
            }
            _componentsToBeRemoved.Clear();
        }

        internal void RemoveComponentsOfEntity(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            entity.TypeBits = 0;
            _entityWorld.RefreshEntity(entity);

            var entityId = entity.Id;
            for (var index = _componentsByType.Count - 1; index >= 0; --index)
            {
                var components = _componentsByType.Get(index);
                if (components != null && entityId < components.Count)
                {
                    var componentToBeRemoved = components.Get(entityId);
                    if (RemovedComponentEvent != null && componentToBeRemoved != null)
                    {
                        RemovedComponentEvent(entity, componentToBeRemoved);
                    }

                    components.Set(entityId, null);
                }
            }
        }

        private void EntityManagerRemovedComponentEvent(Entity entity, IComponent component)
        {
            if (component is ComponentPoolable componentPoolable)
            {
                if (componentPoolable.PoolId < 0)
                {
                    return;
                }

                var pool = _entityWorld.GetPool(component.GetType());
                pool?.ReturnObject(componentPoolable);
            }
        }
    }
}