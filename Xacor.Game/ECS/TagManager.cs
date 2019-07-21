using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Xacor.Game.ECS
{
    public sealed class TagManager
    {
        private readonly Dictionary<string, Entity> _entityByTag;

        internal TagManager()
        {
            _entityByTag = new Dictionary<string, Entity>();
        }

        public Entity GetEntity(string tag)
        {
            Debug.Assert(!string.IsNullOrEmpty(tag), "Tag must not be null or empty.");

            _entityByTag.TryGetValue(tag, out var entity);
            if (entity != null && entity.IsActive)
            {
                return entity;
            }

            Unregister(tag);
            return null;
        }

        public string GetTagOfEntity(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            string tag = null;
            foreach (var pair in _entityByTag.Where(pair => pair.Value.Equals(entity)))
            {
                tag = pair.Key;
                break;
            }

            return tag;
        }

        public bool IsRegistered(string tag)
        {
            Debug.Assert(!string.IsNullOrEmpty(tag), "Tag must not be null or empty.");

            return _entityByTag.ContainsKey(tag);
        }

        internal void Register(string tag, Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");
            Debug.Assert(!string.IsNullOrEmpty(tag), "Tag must not be null or empty.");

            _entityByTag.Add(tag, entity);
        }

        internal void Unregister(string tag)
        {
            Debug.Assert(!string.IsNullOrEmpty(tag), "Tag must not be null or empty.");

            _entityByTag.Remove(tag);
        }

        internal void Unregister(Entity entity)
        {
            var tag = GetTagOfEntity(entity);
            if (!string.IsNullOrEmpty(tag))
            {
                _entityByTag.Remove(tag);
            }
        }
    }
}