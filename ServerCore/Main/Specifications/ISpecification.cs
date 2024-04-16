using System.Collections.Generic;

namespace ServerCore.Main.Specifications
{
    public interface ISpecification
    {
        string Id { get; }
        void Fill(IDictionary<string, object> node);
    }
}