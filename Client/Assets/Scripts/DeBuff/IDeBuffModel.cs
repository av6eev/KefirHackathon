using DeBuff.Specification;
using Save;

namespace DeBuff
{
    public interface IDeBuffModel : ISaveModel
    {
        DeBuffSpecification Specification { get; }
    }
}