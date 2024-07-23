using System.Collections.Generic;
using Reactive.Event;
using Utilities.Model;

namespace Utilities.ModelCollection
{
    public class ModelCollection<T> : IModelCollection<T> where T : IModel
    {
        public readonly ReactiveEvent<T> AddEvent = new(); 
        public readonly ReactiveEvent<T> RemoveEvent = new();

        public ReactiveEvent ChangeEvent { get; } = new();

        public bool IsEmpty => Collection.Count == 0;
        public int Count => Collection.Count;

        protected readonly List<T> Collection = new();

        public void Add(T model)
        {
            Collection.Add(model);
            
            AddEvent.Invoke(model);
            ChangeEvent.Invoke();
        }

        public void Remove(T model)
        {
            Collection.Remove(model);
            
            RemoveEvent.Invoke(model);
            ChangeEvent.Invoke();
        }

        public T GetModel(int index)
        {
            return Collection[index];
        }
        
        public IEnumerable<T> GetModels()
        {
            foreach (var element in Collection)
            {
                yield return element;
            }
        }

        public void Clear()
        {
            foreach (var model in Collection)
            {
                RemoveEvent.Invoke(model);
            }
            
            Collection.Clear();
        }
    }
    
    public class ModelCollection<TKey, TValue> : IModelCollection<TKey, TValue> where TValue : IModel
    {
        public readonly ReactiveEvent<TValue> AddEvent = new(); 
        public readonly ReactiveEvent<TValue> RemoveEvent = new();

        public ReactiveEvent ChangeEvent { get; } = new();

        public bool IsEmpty => Collection.Count == 0;
        protected readonly Dictionary<TKey, TValue> Collection = new();

        public void Add(TKey key, TValue model)
        {
            Collection.Add(key, model);
            
            AddEvent.Invoke(model);
            ChangeEvent.Invoke();
        }

        public void Remove(TKey key)
        {
            var model = Collection[key];
            Collection.Remove(key);
            
            RemoveEvent.Invoke(model);
            ChangeEvent.Invoke();
        }

        public TValue GetModel(TKey key)
        {
            return Collection[key];
        }

        public bool TryGetModel(TKey key, out TValue value)
        {
            if (Collection.TryGetValue(key, out var data))
            {
                value = data;
                return true;
            }

            value = default;
            return false;
        }
        
        public IEnumerable<TValue> GetModels()
        {
            foreach (var element in Collection.Values)
            {
                yield return element;
            }
        }

        public bool Contains(TKey id) => Collection.ContainsKey(id); 
        
        public void Clear()
        {
            Collection.Clear();
        }
    }
}