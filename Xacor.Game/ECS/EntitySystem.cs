using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Xacor.Game.ECS
{
    public abstract class EntitySystem
    {
        protected EntityWorld entityWorld;

        private IDictionary<int, Entity> _actives;

        private readonly Aspect _aspect;

        static EntitySystem()
        {
            BlackBoard = new BlackBoard();
        }

        protected EntitySystem()
        {
            Bit = 0;
            _aspect = Aspect.Empty();
            IsEnabled = true;
        }

        protected EntitySystem(Aspect aspect)
            : this()
        {
            Debug.Assert(aspect != null, "Aspect must not be null.");
            this._aspect = aspect;
        }

        public static BlackBoard BlackBoard { get; protected set; }

        public IEnumerable<Entity> ActiveEntities => _actives.Values;

        public EntityWorld EntityWorld
        {
            get => entityWorld;

            protected internal set
            {
                entityWorld = value;
                if (EntityWorld.IsSortedEntities)
                {
                    _actives = new SortedDictionary<int, Entity>();
                }
                else
                {
                    _actives = new Dictionary<int, Entity>();
                }
            }
        }

        public bool IsEnabled { get; set; }

        internal BigInteger Bit { get; set; }

        public Aspect Aspect => _aspect;

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void OnAdded(Entity entity)
        {
        }

        public virtual void OnChange(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            var contains = (Bit & entity.SystemBits) == Bit;
            var interest = Aspect.Interests(entity);

            if (interest && !contains)
            {
                Add(entity);
            }
            else if (!interest && contains)
            {
                Remove(entity);
            }
            else if (interest && contains && entity.IsEnabled)
            {
                Enable(entity);
            }
            else if (interest && contains && !entity.IsEnabled)
            {
                Disable(entity);
            }
        }

        public virtual void OnDisabled(Entity entity)
        {
        }

        public virtual void OnEnabled(Entity entity)
        {
        }

        public virtual void OnRemoved(Entity entity)
        {
        }

        public virtual void Process()
        {
            if (CheckProcessing())
            {
                Begin();
                ProcessEntities(_actives);
                End();
            }
        }

        public void Toggle()
        {
            IsEnabled = !IsEnabled;
        }

        protected void Add(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            entity.AddSystemBit(Bit);
            if (entity.IsEnabled)
            {
                Enable(entity);
            }

            OnAdded(entity);
        }

        protected virtual void Begin()
        {
        }

        protected virtual bool CheckProcessing()
        {
            return IsEnabled;
        }

        protected virtual void End()
        {
        }

        protected virtual bool Interests(Entity entity)
        {
            return Aspect.Interests(entity);
        }

        protected virtual void ProcessEntities(IDictionary<int, Entity> entities)
        {
        }

        protected void Remove(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            entity.RemoveSystemBit(Bit);
            if (entity.IsEnabled)
            {
                Disable(entity);
            }

            OnRemoved(entity);
        }

        private void Disable(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            if (!_actives.ContainsKey(entity.Id))
            {
                return;
            }

            _actives.Remove(entity.Id);
            OnDisabled(entity);
        }

        private void Enable(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            if (_actives.ContainsKey(entity.Id))
            {
                return;
            }

            _actives.Add(entity.Id, entity);
            OnEnabled(entity);
        }
    }
}