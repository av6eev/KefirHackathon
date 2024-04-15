using System.Collections.Generic;
using Specification;

namespace Specifications.Collection
{
    public interface ISpecificationsCollection<T> : IPrimitiveSpecificationsCollection where T : ISpecification
    {
        T this[string key] { get; }
        int Count { get; }
        Dictionary<string, T> GetSpecifications();
    }
}