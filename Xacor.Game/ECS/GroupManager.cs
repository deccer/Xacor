using System.Collections.Generic;
using System.Diagnostics;
using Xacor.Collections;

namespace Xacor.Game.ECS
{
    public sealed class GroupManager
    {
        private readonly Bag<Entity> _emptyBag;

        private readonly Dictionary<string, Bag<Entity>> _entitiesByGroup;

        private readonly Bag<string> _groupByEntity;

        internal GroupManager()
        {
            _groupByEntity = new Bag<string>();
            _entitiesByGroup = new Dictionary<string, Bag<Entity>>();
            _emptyBag = new Bag<Entity>();
        }

        public Bag<Entity> GetEntities(string group)
        {
            Debug.Assert(!string.IsNullOrEmpty(group), "Group must not be null or empty.");

            return !_entitiesByGroup.TryGetValue(@group, out var bag) ? _emptyBag : bag;
        }

        public string GetGroupOf(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            var entityId = entity.Id;
            return entityId < _groupByEntity.Capacity ? _groupByEntity.Get(entityId) : null;
        }

        public bool IsGrouped(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            return GetGroupOf(entity) != null;
        }

        internal void Remove(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            var entityId = entity.Id;
            if (entityId < _groupByEntity.Capacity)
            {
                var group = _groupByEntity.Get(entityId);
                if (group != null)
                {
                    _groupByEntity.Set(entityId, null);

                    if (_entitiesByGroup.TryGetValue(group, out var entities))
                    {
                        entities.Remove(entity);
                    }
                }
            }
        }

        internal void Set(string group, Entity entity)
        {
            Debug.Assert(!string.IsNullOrEmpty(group), "Group must not be null or empty.");
            Debug.Assert(entity != null, "Entity must not be null.");

            // Entity can only belong to one group.
            Remove(entity);

            if (!_entitiesByGroup.TryGetValue(group, out var entities))
            {
                entities = new Bag<Entity>();
                _entitiesByGroup.Add(group, entities);
            }

            entities.Add(entity);

            _groupByEntity.Set(entity.Id, group);
        }
    }
}