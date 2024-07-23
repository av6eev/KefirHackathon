using System.Collections.Generic;
using ServerCore.Main.Specifications.Base;

namespace ServerCore.Main.Specifications.Collection
{
    public interface INewSpecificationsCollection<T> : IPrimitiveSpecificationsCollection where T : ISpecification
    {
        T this[string key] { get; }
        int Count { get; }
        Dictionary<string, T> GetSpecifications();
    }
}