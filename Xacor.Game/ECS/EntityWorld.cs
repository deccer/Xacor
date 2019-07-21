using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xacor.Collections;

namespace Xacor.Game.ECS
{
    public sealed class EntityWorld
    {
        private readonly Bag<Entity> _deleted;

        private readonly Dictionary<string, IEntityTemplate> _entityTemplates;

        private readonly Dictionary<Type, IComponentPool<ComponentPoolable>> _pools;

        private readonly HashSet<Entity> _refreshed;

        private DateTime _dateTime;

        private int _poolCleanupDelayCounter;

        private bool _isInitialized = false;

        public EntityWorld(bool isSortedEntities = false, bool processAttributes = true, bool initializeAll = false)
        {
            IsSortedEntities = isSortedEntities;
            _refreshed = new HashSet<Entity>();
            _pools = new Dictionary<Type, IComponentPool<ComponentPoolable>>();
            _entityTemplates = new Dictionary<string, IEntityTemplate>();
            _deleted = new Bag<Entity>();
            EntityManager = new EntityManager(this);
            SystemManager = new SystemManager(this);
            TagManager = new TagManager();
            GroupManager = new GroupManager();
            PoolCleanupDelay = 10;
            _dateTime = FastDateTime.Now;
            if (initializeAll)
            {
                InitializeAll(processAttributes);
            }
        }

        public Dictionary<Entity, Bag<IComponent>> CurrentState
        {
            get
            {
                var entities = EntityManager.ActiveEntities;
                var currentState = new Dictionary<Entity, Bag<IComponent>>();
                for (int index = 0, j = entities.Count; index < j; ++index)
                {
                    var entity = entities.Get(index);
                    if (entity != null)
                    {
                        var components = entity.Components;
                        currentState.Add(entity, components);
                    }
                }

                return currentState;
            }
        }

        public long Delta { get; private set; }

        public EntityManager EntityManager { get; private set; }

        public GroupManager GroupManager { get; private set; }

        public int PoolCleanupDelay { get; set; }

        public SystemManager SystemManager { get; private set; }

        public TagManager TagManager { get; private set; }

        internal bool IsSortedEntities { get; private set; }

        public void Clear()
        {
            foreach (var activeEntity in EntityManager.ActiveEntities.Where(activeEntity => activeEntity != null))
            {
                activeEntity.Delete();
            }

            Update();
        }

        public Entity CreateEntity(long? entityUniqueId = null)
        {
            return EntityManager.Create(entityUniqueId);
        }

        public Entity CreateEntityFromTemplate(string entityTemplateTag, params object[] templateArgs)
        {
            return CreateEntityFromTemplate(null, entityTemplateTag, templateArgs);
        }

        public Entity CreateEntityFromTemplate(long entityUniqueId, string entityTemplateTag, params object[] templateArgs)
        {
            return CreateEntityFromTemplate((long?)entityUniqueId, entityTemplateTag, templateArgs);
        }

        public void DeleteEntity(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            _deleted.Add(entity);
        }

        public IComponent GetComponentFromPool(Type type)
        {
            Debug.Assert(type != null, "Type must not be null.");

            if (!_pools.ContainsKey(type))
            {
                throw new Exception("There is no pool for the specified type " + type);
            }

            return _pools[type].New();
        }

        public T GetComponentFromPool<T>() where T : ComponentPoolable
        {
            return (T)GetComponentFromPool(typeof(T));
        }

        public Entity GetEntity(int entityId)
        {
            Debug.Assert(entityId >= 0, "Id must be at least 0.");

            return EntityManager.GetEntity(entityId);
        }

        public IComponentPool<ComponentPoolable> GetPool(Type type)
        {
            Debug.Assert(type != null, "Type must not be null.");

            return _pools[type];
        }

        public void InitializeAll(params Assembly[] assembliesToScan)
        {
            if (!_isInitialized)
            {
                bool processAttributes = assembliesToScan != null && assembliesToScan.Length > 0;
                SystemManager.InitializeAll(processAttributes, assembliesToScan);
                _isInitialized = true;
            }
        }

        public void InitializeAll(bool processAttributes = false)
        {
            if (!_isInitialized)
            {
                SystemManager.InitializeAll(processAttributes);
                _isInitialized = true;
            }
        }

        public Entity LoadEntityState(string templateTag, string groupName, IEnumerable<IComponent> components, params object[] templateArgs)
        {
            Debug.Assert(components != null, "Components must not be null.");

            var entity = !string.IsNullOrEmpty(templateTag) ? CreateEntityFromTemplate(templateTag, -1, templateArgs) : CreateEntity();

            if (!string.IsNullOrEmpty(groupName))
            {
                GroupManager.Set(groupName, entity);
            }

            foreach (var comp in components)
            {
                entity.AddComponent(comp);
            }

            return entity;
        }

        public void SetEntityTemplate(string entityTag, IEntityTemplate entityTemplate)
        {
            _entityTemplates.Add(entityTag, entityTemplate);
        }

        public void SetPool(Type type, IComponentPool<ComponentPoolable> pool)
        {
            Debug.Assert(type != null, "Type must not be null.");
            Debug.Assert(pool != null, "Component pool must not be null.");

            _pools.Add(type, pool);
        }

        public void Update()
        {
            long deltaTicks = (FastDateTime.Now - _dateTime).Ticks;
            _dateTime = FastDateTime.Now;
            Update(deltaTicks);
        }

        public void Update(long deltaTicks)
        {
            Delta = deltaTicks;

            EntityManager.RemoveMarkedComponents();

            ++_poolCleanupDelayCounter;
            if (_poolCleanupDelayCounter > PoolCleanupDelay)
            {
                _poolCleanupDelayCounter = 0;
                foreach (Type item in _pools.Keys)
                {
                    _pools[item].CleanUp();
                }
            }

            if (!_deleted.IsEmpty)
            {
                for (int index = _deleted.Count - 1; index >= 0; --index)
                {
                    Entity entity = _deleted.Get(index);
                    TagManager.Unregister(entity);
                    GroupManager.Remove(entity);
                    EntityManager.Remove(entity);
                    entity.DeletingState = false;
                }

                _deleted.Clear();
            }

            bool isRefreshing = _refreshed.Count > 0;
            if (isRefreshing)
            {
                foreach (Entity entity in _refreshed)
                {
                    EntityManager.Refresh(entity);
                    entity.RefreshingState = false;
                }

                _refreshed.Clear();
            }

            SystemManager.Update();
        }

        public void Draw()
        {
            SystemManager.Draw();
        }

        public void UnloadContent()
        {
            SystemManager.TerminateAll();
        }

        internal void RefreshEntity(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            _refreshed.Add(entity);
        }

        private Entity CreateEntityFromTemplate(long? entityUniqueId, string entityTemplateTag, params object[] templateArgs)
        {
            Debug.Assert(!string.IsNullOrEmpty(entityTemplateTag), "Entity template tag must not be null or empty.");

            var entity = EntityManager.Create(entityUniqueId);
            _entityTemplates.TryGetValue(entityTemplateTag, out var entityTemplate);
            if (entityTemplate == null)
            {
                throw new MissingEntityTemplateException(entityTemplateTag);
            }

            entity = entityTemplate.BuildEntity(entity, this, templateArgs);
            RefreshEntity(entity);
            return entity;
        }
    }
}