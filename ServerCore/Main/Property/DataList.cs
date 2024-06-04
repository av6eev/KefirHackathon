using System;
using System.Collections.Generic;

namespace ServerCore.Main.Property
{
    public class DataList<T> : IServerData
    {
        public event Action<T> OnAdd;
        public event Action<T> OnRemove;
        public event Action<T> OnChanged;

        public string Id { get; }
        public List<T> Collection { get; } = new();

        public bool IsDirty { get; set; }

        private readonly List<ushort> _addIndices = new();
        private readonly List<ushort> _removeIndices = new();
        private readonly List<ushort> _changeIndices = new();

        public DataList(string id)
        {
            Id = id;
        }

        public void Read(Protocol protocol)
        {
            protocol.Get(out ushort removedCount);

            for (var i = 0; i < removedCount; i++)
            {
                protocol.Get(out ushort index);
                var data = Collection[index];
                Collection.Remove(data);
                
                OnRemove?.Invoke(data);
            }
            
            protocol.Get(out ushort addedCount);

            for (var i = 0; i < addedCount; i++)
            {
                protocol.Get(out ushort index);
                protocol.Get(out T value);

                Collection.Insert(index, value);
                
                OnAdd?.Invoke(value);
            }
            
            protocol.Get(out ushort changedCount);
            
            for (var i = 0; i < changedCount; i++)
            {
                protocol.Get(out ushort index);
                protocol.Get(out T value);
            
                Collection[index] = value;
            
                OnChanged?.Invoke(value);
            }
        }

        public void WriteAll(Protocol protocol)
        {
            protocol.Add((ushort)0);
            protocol.Add((ushort)Collection.Count);

            for (ushort index = 0; index < Collection.Count; index++)
            {
                var element = Collection[index];
            
                protocol.Add(index);
                protocol.Add(element);
            }
        }

        public bool Write(Protocol protocol)
        {
            protocol.Add((ushort)_removeIndices.Count);

            foreach (var index in _removeIndices)
            {
                protocol.Add(index);
            }

            protocol.Add((ushort)_addIndices.Count);

            foreach (var index in _addIndices)
            {
                protocol.Add(index);
                protocol.Add(Collection[index]);
            }

            protocol.Add((ushort)_changeIndices.Count);

            foreach (var index in _changeIndices)
            {
                protocol.Add(index);
                protocol.Add(Collection[index]);
            }
        
            if (_removeIndices.Count > 0 || _addIndices.Count > 0 || _changeIndices.Count > 0)
            {
                _removeIndices.Clear();
                _addIndices.Clear();
                _changeIndices.Clear();
                IsDirty = false;

                return true;
            }

            return false;
        }

        public bool HasChanges()
        {
            return IsDirty;
        }

        public void Add(T data)
        {
            Collection.Add(data);
            _addIndices.Add((ushort)Collection.IndexOf(data));
            IsDirty = true;

            OnAdd?.Invoke(data);
        }

        public void Remove(T data)
        {
            _removeIndices.Add((ushort)Collection.IndexOf(data));
            Collection.Remove(data);
            IsDirty = true;

            OnRemove?.Invoke(data);
        }

        public void Change(int index, T value)
        {
            _changeIndices.Add((ushort)index);
            Collection[index] = value;
            IsDirty = true;
        
            OnChanged?.Invoke(value);
        }

        public bool Contains(T value)
        {
            foreach (var element in Collection)
            {
                if (Equals(element, value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}