using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServerCore.Main.Property
{
    public class DataCollection<T> : IServerData where T : IServerData, new()
    {
        public event Action<T> OnAdd;
        public event Action<T> OnRemove;
        
        public string Id { get; }
        public List<T> Collection { get; } = new();

        public bool IsDirty { get; set; }
        
        private ushort _addCount;
        private List<ushort> _removeCount = new();

        public DataCollection(string id)
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
                var data = new T();
                
                Collection.Add(data);
                data.Read(protocol);
                
                OnAdd?.Invoke(data);
            }
            
            protocol.Get(out ushort changedCount);
            
            for (var i = 0; i < changedCount; i++)
            {
                protocol.Get(out ushort index);
                Collection[index].Read(protocol);
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
                
                element.Write(protocol);
            }
        }

        public bool Write(Protocol protocol)
        {
            protocol.Add((ushort)_removeCount.Count);

            foreach (var element in _removeCount)
            {
                protocol.Add(element);
            }
            
            protocol.Add(_addCount);
            
            ushort changedDataCount = 0;
            ushort index = 0;
            var changedDataset = new Dictionary<ushort, IServerData>();

            foreach (var data in Collection)
            {
                if (data.IsDirty)
                {
                    changedDataCount++;
                    changedDataset.Add(index, data);
                }

                index++;
            }
            
            if (changedDataCount > 0)
            {
                protocol.Add(changedDataCount);

                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Key);
                    data.Value.Write(protocol);
                }
            }

            if (_removeCount.Count > 0 || _addCount > 0 || changedDataCount > 0)
            {
                _removeCount.Clear();
                _addCount = 0;
                IsDirty = false;
                
                return true;
            }
            
            return false;
        }

        public bool HasChanges()
        {
            if (IsDirty)
            {
                return true;
            }

            foreach (var data in Collection)
            {
                if (data.HasChanges())
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(T data)
        {
            Collection.Add(data);
            _addCount++;
            IsDirty = true;
            
            OnAdd?.Invoke(data);
        }

        public void Remove(T data)
        {
            _removeCount.Add((ushort)Collection.IndexOf(data));
            Collection.Remove(data);
            IsDirty = true;
            
            OnRemove?.Invoke(data);
        }
    }
    
    public class DataCollection<TKey, TValue> : IServerData where TValue : IServerData, new()
    {
        public event Action<TValue> OnAdd;
        public event Action<TKey> OnRemove;
        
        public string Id { get; }
        public Dictionary<TKey, TValue> Collection { get; } = new();

        public bool IsDirty { get; set; }

        private List<TKey> _addIds = new();
        private List<TKey> _removeIds = new();

        public DataCollection(string id)
        {
            Id = id;
        }

        public void Read(Protocol protocol)
        {
            protocol.Get(out ushort removedCount);

            for (var i = 0; i < removedCount; i++)
            {
                protocol.Get(out TKey index);
                Collection.Remove(index);
                
                OnRemove?.Invoke(index);
            }
            
            protocol.Get(out ushort addedCount);

            for (var i = 0; i < addedCount; i++)
            {
                protocol.Get(out TKey index);

                var data = new TValue();
                
                Collection.Add(index, data);
                Collection[index].Read(protocol);
                
                OnAdd?.Invoke(data);
            }
            
            protocol.Get(out ushort changedCount);
            
            for (var i = 0; i < changedCount; i++)
            {
                protocol.Get(out TKey index);
                
                Collection[index].Read(protocol);
                
                // if (Collection.TryGetValue(index, out var data))
                // {
                    // data.Read(protocol);
                // }
            }
        }

        public void WriteAll(Protocol protocol)
        {
            protocol.Add((ushort)0);
            protocol.Add((ushort)Collection.Count);
            
            foreach (var element in Collection.Keys)
            {
                protocol.Add(element);
                Collection[element].WriteAll(protocol);
            }
            
            protocol.Add((ushort)0);
        }

        public bool Write(Protocol protocol)
        {
            protocol.Add((ushort)_removeIds.Count);

            foreach (var element in _removeIds)
            {
                protocol.Add(element);
            }
            
            protocol.Add((ushort)_addIds.Count);
            
            foreach (var element in _addIds)
            {
                protocol.Add(element);
                Collection[element].Write(protocol);
            }
            
            ushort changedDataCount = 0;
            var changedDataset = new Dictionary<TKey, IServerData>();

            foreach (var data in Collection.Where(data => data.Value.HasChanges()))
            {
                changedDataCount++;
                changedDataset.Add(data.Key, data.Value);
            }
            
            if (changedDataCount > 0)
            {
                protocol.Add(changedDataCount);

                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Key);
                    data.Value.Write(protocol);
                }
            }

            if (_removeIds.Count > 0 || _addIds.Count > 0 || changedDataCount > 0)
            {
                _removeIds.Clear();
                _addIds.Clear();
                IsDirty = false;
                
                return true;
            }
            
            return false;
        }

        public bool HasChanges()
        {
            if (IsDirty)
            {
                return true;
            }

            foreach (var data in Collection)
            {
                if (data.Value.HasChanges())
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(TKey key, TValue data)
        {
            Collection.Add(key, data);
            _addIds.Add(key);
            IsDirty = true;
            
            OnAdd?.Invoke(data);
        }

        public void Remove(TKey key)
        {
            _removeIds.Add(key);
            Collection.Remove(key);
            IsDirty = true;
            
            OnRemove?.Invoke(key);
        }
    }
}