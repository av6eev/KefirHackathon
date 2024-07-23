using ServerCore.Main.Utilities.Event;

namespace Server.Utilities.ModelCollection
{
    public interface IModelCollection<T> : ICollection
    {
        ReactiveEvent<T> AddEvent { get; }
        ReactiveEvent<T> RemoveEvent { get; }
        
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
        ReactiveEvent<TValue> AddEvent { get; }
        ReactiveEvent<TValue> RemoveEvent { get; }
        
        bool IsEmpty { get; }
        void Add(TKey key, TValue model);
        void Remove(TKey key);
        TValue GetModel(TKey key);
        bool TryGetModel(TKey key, out TValue model);
        IEnumerable<TValue> GetModels();
        bool Contains(TKey id);
        void Clear();
    }
}