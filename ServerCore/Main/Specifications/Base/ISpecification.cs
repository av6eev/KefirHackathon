using System.Collections.Generic;

namespace ServerCore.Main.Specifications.Base
{
    public interface ISpecification
    {
        string Id { get; }
        void Fill(IDictionary<string, object> node);
    }
}