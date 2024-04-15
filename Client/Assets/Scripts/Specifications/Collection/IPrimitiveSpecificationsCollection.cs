using Specification;

namespace Specifications.Collection
{
    public interface IPrimitiveSpecificationsCollection
    {
        void Add(string key, ISpecification element);
    }
}