using Specification;
using UnityEngine;

namespace Specifications
{
    public class SpecificationScrObj<T> : ScriptableObject where T : ISpecification
    {
        public T Specification;
    }
}