using Entities.Specification;
using Reactive.Field;
using UnityEngine;
using Utilities.ModelCollection;

namespace Entities
{
    public abstract class EntityModel : IEntityModel
    {
        public EntitySpecification Specification { get; set; }
        public Vector3 Position { get; set; }
        
        public ReactiveField<bool> IsCanAttack { get; } = new();
        public ReactiveField<float> CurrentSpeed { get; } = new();
        public ReactiveField<int> CurrentHealth { get; } = new();
        public ReactiveField<IEntityModel> Target { get; } = new(null);
        public ReactiveField<bool> InTarget { get; } = new();
        public ReactiveField<Quaternion> NextRotation { get; } = new();
        
        public ModelCollection<EntityResourceType, EntityResource> Resources { get; } = new();
        
        protected EntityModel(EntitySpecification specification, IEntityModel target)
        {
            Specification = specification;
            Target.Value = target;
        }

        protected EntityModel(EntitySpecification specification)
        {
            Specification = specification;
        }
    }
}