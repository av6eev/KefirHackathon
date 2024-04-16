using System.Collections.Generic;

namespace ServerCore.Main.Specifications.Collection
{
    public class NewSpecificationsCollection<T> : INewSpecificationsCollection<T> where T : ISpecification
    {
        public readonly Dictionary<string, T> Specifications = new();

        public T this[string key] => Specifications[key];
        public int Count => Specifications.Count;

        public void Add(string key, ISpecification element)
        {
            Specifications.Add(key, (T)element);   
        }

        public void Clear()
        {
            Specifications.Clear();
        }

        public Dictionary<string, T> GetSpecifications()
        {
            return Specifications;
        }
    }
}