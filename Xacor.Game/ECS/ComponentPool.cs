using System;
using System.Collections.Generic;

namespace Xacor.Game.ECS
{
    public class ComponentPool<T> : IComponentPool<T>
            where T : ComponentPoolable
    {
        private readonly Func<Type, T> _allocate;

        private readonly bool _isResizeAllowed;

        private readonly Type _innerType;

        private readonly List<T> _invalidComponents;

        private T[] _components;

        public ComponentPool(int initialSize, int resizePool, bool resizes, Func<Type, T> allocateFunc, Type innerType)
        {
            _invalidComponents = new List<T>();

            if (initialSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(initialSize), "initialSize must be at least 1.");
            }

            if (resizePool < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(resizePool), "resizePool must be at least 1.");
            }

            if (innerType == null)
            {
                throw new ArgumentNullException(nameof(innerType));
            }

            _innerType = innerType;
            _isResizeAllowed = resizes;
            ResizeAmount = resizePool;

            _components = new T[initialSize];
            InvalidCount = _components.Length;

            _allocate = allocateFunc ?? throw new ArgumentNullException(nameof(allocateFunc));
        }

        public int InvalidCount { get; private set; }

        public int ResizeAmount { get; internal set; }

        public int ValidCount => _components.Length - InvalidCount;

        public T this[int index]
        {
            get
            {
                index += InvalidCount;

                if (index < InvalidCount || index >= _components.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "The index must be less than or equal to ValidCount.");
                }

                return _components[index];
            }
        }

        public void CleanUp()
        {
            foreach (T component in _invalidComponents)
            {
                // otherwise if we're not at the start of the invalid objects, we have to move
                // the object to the invalid object section of the array
                if (component.PoolId != InvalidCount)
                {
                    _components[component.PoolId] = _components[InvalidCount];
                    _components[InvalidCount].PoolId = component.PoolId;
                    _components[InvalidCount] = component;
                    component.PoolId = -1;
                }

                // clean the object if desired
                component.CleanUp();
                ++InvalidCount;
            }

            _invalidComponents.Clear();
        }

        public T New()
        {
            // If we're out of invalid instances...
            if (InvalidCount == 0)
            {
                // If we can't resize, then we can not give the user back any instance.
                if (!_isResizeAllowed)
                {
                    throw new Exception("Limit Exceeded " + _components.Length + ", and the pool was set to not resize.");
                }

                // Create a new array with some more slots and copy over the existing components.
                var newComponents = new T[_components.Length + ResizeAmount];

                for (int index = _components.Length - 1; index >= 0; --index)
                {
                    if (index >= InvalidCount)
                    {
                        _components[index].PoolId = index + ResizeAmount;
                    }

                    newComponents[index + ResizeAmount] = _components[index];
                }

                _components = newComponents;

                // move the invalid count based on our resize amount
                InvalidCount += ResizeAmount;
            }

            // decrement the counter
            --InvalidCount;

            // get the next component in the list
            T result = _components[InvalidCount];

            // if the component is null, we need to allocate a new instance
            if (result == null)
            {
                result = _allocate(_innerType);

                _components[InvalidCount] = result ?? throw new InvalidOperationException("The pool's allocate method returned a null object reference.");
            }

            result.PoolId = InvalidCount;

            // Initialize the object if a delegate was provided.
            result.Initialize();

            return result;
        }

        public void ReturnObject(T component)
        {
            _invalidComponents.Add(component);
        }
    }
}