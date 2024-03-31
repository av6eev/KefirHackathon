using System.Collections.Generic;
using DeBuff.Specification;
using Reactive.Field;

namespace DeBuff
{
    public class DeBuffModel : IDeBuffModel
    {
        public DeBuffSpecification Specification { get; }
        public ReactiveField<bool> IsActive { get; } = new(false);

        public DeBuffModel(DeBuffSpecification specification)
        {
            Specification = specification;
        }

        public IDictionary<string, object> GetSaveData()
        {
            return new Dictionary<string, object>();
        }
    }
}