using System.Collections.Generic;
using Specification;
using UnityEngine;

namespace Specifications
{
    public class SpecificationCollectionScrObj<TSpecification> : ScriptableObject where TSpecification : ISpecification
    {
        public List<SpecificationScrObj<TSpecification>> Collection;
    }
}