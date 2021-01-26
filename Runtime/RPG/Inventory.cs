using BaseTool.RPG.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace BaseTool.RPG
{
    [Serializable]
    public class Inventory<T> : ICollection<T>, IEnumerable<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T> where T : ItemInventory
    {
        private List<T> _items;

        public int MaxSize { get; private set; }

        public bool IsFixedSize => true;

        #region INTERFACES

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public int Count => _items.Count;

        public bool IsReadOnly { get; private set; }

        public void Add(T item)
        {
            if (Count == MaxSize)
            {
                throw new System.Exception("The inventory is already full.");
            }
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        public Inventory()
        {
            _items = new List<T>();
        }

        public Inventory(List<T> items)
        {
            _items = new List<T>(items);
        }

        public Inventory(int maxSize) : this()
        {
            MaxSize = maxSize;
        }

        public Inventory(List<T> items, int maxSize)
        {
            if(items.Count > maxSize)
            {
                throw new System.Exception("The list is too large for the inventory."); 
            }

            _items = new List<T>(items);
            MaxSize = maxSize;
        }
    }
}
