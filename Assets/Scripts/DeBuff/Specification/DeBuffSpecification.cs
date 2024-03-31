using System;
using Specification;

namespace DeBuff.Specification
{
    [Serializable]
    public class DeBuffSpecification : BaseSpecification
    {
        public string Title;
        public float ApplyValue;
        public DeBuffType Type;
    }
}