using System.Collections;
using System.Collections.Generic;

namespace Xacor.Collections
{
    internal class BagEnumerator<T> : IEnumerator<T>
    {
        private volatile Bag<T> _bag;

        private volatile int _index;

        public BagEnumerator(Bag<T> bag)
        {
            _bag = bag;
            Reset();
        }

        T IEnumerator<T>.Current => _bag.Get(_index);

        object IEnumerator.Current => _bag.Get(_index);

        public void Dispose()
        {
            _bag = null;
        }

        public bool MoveNext()
        {
            return ++_index < _bag.Count;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}