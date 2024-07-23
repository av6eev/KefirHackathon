using System.Collections.Generic;

namespace Utilities.ModelCollection
{
    public interface IModelCollection<T> : ICollection
    {
        int Count { get; }
        bool IsEmpty { get; }
        void Add(T model);
        void Remove(T model);
        T GetModel(int index);
        IEnumerable<T> GetModels();
        void Clear();
    }
    
    public interface IModelCollection<in TKey, TValue> : ICollection
    {
        bool IsEmpty { get; }
        void Add(TKey key, TValue model);
        void Remove(TKey key);
        TValue GetModel(TKey key);
        bool TryGetModel(TKey key, out TValue value);
        IEnumerable<TValue> GetModels();
        bool Contains(TKey id);
        void Clear();
    }
}