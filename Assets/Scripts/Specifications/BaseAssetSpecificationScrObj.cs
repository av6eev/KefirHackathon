using Specification;
using UnityEngine;

namespace Specifications
{
    public abstract class BaseAssetSpecificationScrObj : ScriptableObject
    {
        public abstract ISpecification GetSpecification();
    }
}