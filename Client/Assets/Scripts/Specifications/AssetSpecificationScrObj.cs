using Specification;

namespace Specifications
{
    public class AssetSpecificationScrObj<T> : BaseAssetSpecificationScrObj where T : ISpecification
    {
        public T Specification;
        
        public override ISpecification GetSpecification()
        {
            return Specification;
        }
    }
}