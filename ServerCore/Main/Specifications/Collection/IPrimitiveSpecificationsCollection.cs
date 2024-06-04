using ServerCore.Main.Specifications.Base;

namespace ServerCore.Main.Specifications.Collection
{
    public interface IPrimitiveSpecificationsCollection
    {
        void Add(string key, ISpecification element); 
    }
}