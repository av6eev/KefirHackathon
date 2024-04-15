using System.Collections.Generic;
using Specification;

namespace Specifications.Collection
{
    public class SpecificationsCollection<T> : ISpecificationsCollection<T> where T : ISpecification
    {
        private readonly Dictionary<string, T> _specifications = new();

        public T this[string key] => _specifications[key];
        public int Count => _specifications.Count;

        public void Add(string key, ISpecification element)
        {
            _specifications.Add(key, (T)element);   
        }

        public void Clear()
        {
            _specifications.Clear();
        }

        public Dictionary<string, T> GetSpecifications()
        {
            return _specifications;
        }
    }
}