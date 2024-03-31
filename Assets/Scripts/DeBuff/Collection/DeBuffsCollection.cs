using System.Collections.Generic;
using DeBuff.Specification;
using Utilities.ModelCollection;

namespace DeBuff.Collection
{
    public class DeBuffsCollection : ModelCollection<DeBuffType, DeBuffModel>, IDeBuffsCollection
    {
        public DeBuffsCollection(Dictionary<string, DeBuffSpecification> specifications)
        {
            foreach (var specification in specifications.Values)
            {
                Add(specification.Type, new DeBuffModel(specification));
            }
        }
    }
}