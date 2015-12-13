using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ1_ZAD1
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
    public interface IIntegerList
    {
        ///Adds an item to the collection.
        void Add(int item);

        ///Removes the 1st occurrence of the item --> does nothing if it doesn't find the item
        bool Remove(int item);

        ///Removes the item on a given index
        bool RemoveAt(int index);

        /// Returns the item at the given index
        int GetElement(int index);

        /// Returns the 1st index of an item or -1 if the item is nonexistent
        int IndexOf(int item);

        /// A readonly property. Holds the number of items
        int Count { get; }

        /// Removes all items form the collection
        void Clear();

        /// Determines if the item is existent
        bool Contains(int item);
    }
    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int _sizeOf;
        private int _current;

        public IntegerList()
        {
            this._internalStorage = new int[4];
            this._sizeOf = 4;
            this._current = 0;
        }
        public IntegerList(int numberOfItems)
        {
            this._internalStorage = new int[numberOfItems];
            this._sizeOf = numberOfItems;
            this._current = 0;
        }
        public void Add(int item)
        {
            if (_current == (_sizeOf - 1))
            {
                int[] _helper = _internalStorage;
                _internalStorage = new int[_sizeOf * 2];
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
            if (index>=_sizeOf)
            {
                return false;
            }
            for (int i = index; i < _sizeOf-1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            return true;
        }
        public bool Remove(int item)
        {
                if (Contains(item))
                {
                    return RemoveAt(IndexOf(item));
                }
            return false;
        }
        public int GetElement(int index)
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
        public int IndexOf(int item)
        {
            for (int i = 0; i < _sizeOf; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool Contains(int item)
        {
            if (IndexOf(item)!= -1)
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


    }
}
