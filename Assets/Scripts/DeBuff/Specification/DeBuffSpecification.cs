using System;
using Specification;

namespace DeBuff.Specification
{
    [Serializable]
    public class DeBuffSpecification : BaseSpecification
    {
        public string Title;
        public string DialogText;
        public float ApplyValue;
        public int Chance;
        public DeBuffType Type;
    }
}