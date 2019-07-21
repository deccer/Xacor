using System;

namespace Xacor.Game.ECS
{
    public class ComponentPoolMultiThread<T> : IComponentPool<T>
            where T : ComponentPoolable
    {
        private readonly ComponentPool<T> _pool;

        private readonly object _sync;

        public ComponentPoolMultiThread(int initialSize, int resizePool, bool resizes, Func<Type, T> allocateFunc, Type innerType)
        {
            _pool = new ComponentPool<T>(initialSize, resizePool, resizes, allocateFunc, innerType);
            _sync = new object();
        }

        public int InvalidCount => _pool.InvalidCount;

        public int ResizeAmount => _pool.ResizeAmount;

        public int ValidCount => _pool.ValidCount;

        public T this[int index]
        {
            get
            {
                lock (_sync)
                {
                    return _pool[index];
                }
            }
        }

        public void CleanUp()
        {
            lock (_sync)
            {
                _pool.CleanUp();
            }
        }

        public T New()
        {
            lock (_sync)
            {
                return _pool.New();
            }
        }

        public void ReturnObject(T component)
        {
            lock (_sync)
            {
                _pool.ReturnObject(component);
            }
        }
    }
}