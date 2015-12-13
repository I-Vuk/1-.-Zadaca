using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ1_ZAD4
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public interface IGenericList<X>:IEnumerable<X>
    {
        ///Adds an item to the collection.
        void Add(X item);

        ///Removes the 1st occurrence of the item --> does nothing if it doesn't find the item
        bool Remove(X item);

        ///Removes the item on a given index
        bool RemoveAt(int index);

        /// Returns the item at the given index
        X GetElement(int index);

        /// Returns the 1st index of an item or -1 if the item is nonexistent
        int IndexOf(X item);

        /// A readonly property. Holds the number of items
        int Count { get; }

        /// Removes all items form the collection
        void Clear();

        /// Determines if the item is existent
        bool Contains(X item);
    }
    public class GenericList<X> : IGenericList<X>
    {
        private X[] _internalStorage;
        private int _sizeOf;
        private int _current;

        public GenericList()
        {
            this._internalStorage = new X[4];
            this._sizeOf = 4;
            this._current = 0;
        }
        public GenericList(int numberOfItems)
        {
            this._internalStorage = new X[numberOfItems];
            this._sizeOf = numberOfItems;
            this._current = 0;
        }
        public void Add(X item)
        {
            if (_current == (_sizeOf - 1))
            {
                X[] _helper = _internalStorage;
                _internalStorage = new X[_sizeOf * 2];
                for (int i = 0; i < _sizeOf; i++)
                {
                    _internalStorage[i] = _helper[i];
                }
                _sizeOf *= 2;
            }
            _internalStorage[_current++] = item;
        }
        public bool RemoveAt(int index)
        {
            if (index >= _sizeOf)
            {
                return false;
            }
            for (int i = index; i < _sizeOf - 1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            return true;
        }
        public bool Remove(X item)
        {
            if (Contains(item))
            {
                return RemoveAt(IndexOf(item));
            }
            return false;
        }
        public X GetElement(int index)
        {
            try
            {
                return _internalStorage[index];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Ne postoji taj index.");
                throw e;
            }

        }
        public int Count
        {
            get { return _current; }
        }
        public int IndexOf(X item)
        {
            for (int i = 0; i < _sizeOf; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }
        public bool Contains(X item)
        {
            if (IndexOf(item) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Clear()
        {
            _internalStorage = null;
            _sizeOf = 0;
            _current = 0;
        }
        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }

    internal class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> _genericList;
        private int _current=0;

        public GenericListEnumerator(GenericList<X> genericList)
        {
            _genericList = genericList;
        }
        public bool MoveNext()
        {
            if (_genericList.Count() == _current)
            {
                _current = 0;
                return false;
            }
            else
            {
                ++_current;
                return true; 
            }
        }
        public X Current
        {
            get
            {
                return _genericList.GetElement(_current);
            }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }
        public void Dispose()
        {
            //Ignoring
        }
        public void Reset()
        {
            //Ignoring
        }
    }
}
