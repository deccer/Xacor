using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Xacor.Collections;

namespace Xacor.Game.ECS
{
    public sealed class Entity
    {
        private readonly EntityManager _entityManager;

        private readonly EntityWorld _entityWorld;

        private long _uniqueId;

        internal Entity(EntityWorld entityWorld, int id)
        {
            SystemBits = 0;
            TypeBits = 0;
            IsEnabled = true;
            this._entityWorld = entityWorld;
            _entityManager = entityWorld.EntityManager;
            Id = id;
        }

        public Bag<IComponent> Components => _entityManager.GetComponents(this);

        public bool DeletingState { get; set; }

        public bool IsEnabled { get; set; }

        public string Group
        {
            get => _entityWorld.GroupManager.GetGroupOf(this);

            set => _entityWorld.GroupManager.Set(value, this);
        }

        public int Id { get; private set; }

        public bool RefreshingState { get; set; }

        public string Tag
        {
            get => _entityWorld.TagManager.GetTagOfEntity(this);

            set
            {
                var oldTag = _entityWorld.TagManager.GetTagOfEntity(this);
                if (value != oldTag)
                {
                    if (oldTag != null)
                    {
                        _entityWorld.TagManager.Unregister(this);
                    }

                    if (value != null)
                    {
                        _entityWorld.TagManager.Register(value, this);
                    }
                }
            }
        }

        public long UniqueId
        {
            get => _uniqueId;

            internal set
            {
                Debug.Assert(_uniqueId >= 0, "UniqueId must be at least 0.");

                _uniqueId = value;
            }
        }

        public bool IsActive => _entityManager.IsActive(Id);

        internal BigInteger SystemBits { get; set; }

        internal BigInteger TypeBits { get; set; }

        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component must not be null.");

            _entityManager.AddComponent(this, component);
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            Debug.Assert(component != null, "Component must not be null.");

            _entityManager.AddComponent<T>(this, component);
        }

        public T AddComponentFromPool<T>() where T : ComponentPoolable
        {
            var component = _entityWorld.GetComponentFromPool<T>();
            _entityManager.AddComponent<T>(this, component);
            return component;
        }

        public void AddComponentFromPool<T>(System.Action<T> init) where T : ComponentPoolable
        {
            Debug.Assert(init != null, "Init delegate must not be null.");

            var component = _entityWorld.GetComponentFromPool<T>();
            init(component);
            _entityManager.AddComponent<T>(this, component);
        }

        public void Delete()
        {
            if (DeletingState)
            {
                return;
            }

            _entityWorld.DeleteEntity(this);
            DeletingState = true;
        }

        public IComponent GetComponent(ComponentType componentType)
        {
            Debug.Assert(componentType != null, "Component type must not be null.");

            return _entityManager.GetComponent(this, componentType);
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)_entityManager.GetComponent(this, ComponentType<T>.CType);
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return !Equals((T)_entityManager.GetComponent(this, ComponentType<T>.CType), default(T));
        }

        public void Refresh()
        {
            if (RefreshingState)
            {
                return;
            }

            _entityWorld.RefreshEntity(this);
            RefreshingState = true;
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            _entityManager.RemoveComponent(this, ComponentTypeManager.GetTypeFor<T>());
        }

        public void RemoveComponent(ComponentType componentType)
        {
            Debug.Assert(componentType != null, "Component type must not be null.");

            _entityManager.RemoveComponent(this, componentType);
        }

        public void Reset()
        {
            SystemBits = 0;
            TypeBits = 0;
            IsEnabled = true;
        }

        public override string ToString()
        {
            return $"Entity{{{Id}}}";
        }

        internal void AddSystemBit(BigInteger bit)
        {
            SystemBits |= bit;
        }

        internal void AddTypeBit(BigInteger bit)
        {
            TypeBits |= bit;
        }

        internal void RemoveSystemBit(BigInteger bit)
        {
            SystemBits &= ~bit;
        }

        internal void RemoveTypeBit(BigInteger bit)
        {
            TypeBits &= ~bit;
        }
    }
}