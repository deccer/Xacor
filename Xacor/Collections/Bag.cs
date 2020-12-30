using System;
using System.Collections;
using System.Collections.Generic;

namespace Xacor.Collections
{
    public class Bag<T> : IEnumerable<T>
    {
        private T[] _elements;

        public Bag(int capacity = 16)
        {
            _elements = new T[capacity];
            Count = 0;
        }

        public int Capacity => _elements.Length;

        public bool IsEmpty => Count == 0;

        public int Count { get; private set; }

        public T this[int index]
        {
            get => _elements[index];

            set
            {
                if (index >= _elements.Length)
                {
                    Grow(index * 2);
                    Count = index + 1;
                }
                else if (index >= Count)
                {
                    Count = index + 1;
                }

                _elements[index] = value;
            }
        }

        public void Add(T element)
        {
            // is size greater than capacity increase capacity
            if (Count == _elements.Length)
            {
                Grow();
            }

            _elements[Count] = element;
            ++Count;
        }

        public void AddRange(Bag<T> rangeOfElements)
        {
            for (int index = 0, j = rangeOfElements.Count; j > index; ++index)
            {
                Add(rangeOfElements.Get(index));
            }
        }

        public void Clear()
        {
            // Null all elements so garbage collector can clean up.
            for (int index = Count - 1; index >= 0; --index)
            {
                _elements[index] = default(T);
            }

            Count = 0;
        }

        public bool Contains(T element)
        {
            for (int index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_elements[index]))
                {
                    return true;
                }
            }

            return false;
        }

        public T Get(int index)
        {
            return _elements[index];
        }

        public T Remove(int index)
        {
            // Make copy of element to remove so it can be returned.
            var result = _elements[index];
            --Count;

            // Overwrite item to remove with last element.
            _elements[index] = _elements[Count];

            // Null last element, so garbage collector can do its work.
            _elements[Count] = default(T);
            return result;
        }

        public bool Remove(T element)
        {
            for (int index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_elements[index]))
                {
                    --Count;

                    // Overwrite item to remove with last element.
                    _elements[index] = _elements[Count];
                    _elements[Count] = default(T);

                    return true;
                }
            }

            return false;
        }

        public bool RemoveAll(Bag<T> bag)
        {
            bool isResult = false;
            for (int index = bag.Count - 1; index >= 0; --index)
            {
                if (Remove(bag.Get(index)))
                {
                    isResult = true;
                }
            }

            return isResult;
        }

        public T RemoveLast()
        {
            if (Count > 0)
            {
                --Count;
                T result = _elements[Count];

                // default(T) if class = null.
                _elements[Count] = default(T);
                return result;
            }

            return default(T);
        }

        public void Set(int index, T element)
        {
            if (index >= _elements.Length)
            {
                Grow(index * 2);
                Count = index + 1;
            }
            else if (index >= Count)
            {
                Count = index + 1;
            }

            _elements[index] = element;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new BagEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BagEnumerator<T>(this);
        }

        private void Grow()
        {
            Grow((int)(_elements.Length * 1.5) + 1);
        }

        private void Grow(int newCapacity)
        {
            var oldElements = _elements;
            _elements = new T[newCapacity];
            Array.Copy(oldElements, 0, _elements, 0, oldElements.Length);
        }
    }
}
