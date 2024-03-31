using Entities.Animation;
using Entities.Specification;
using Reactive.Event;
using Reactive.Field;
using UnityEngine;
using Utilities.ModelCollection;

namespace Entities
{
    public abstract class EntityModel : IEntityModel
    {
        public EntitySpecification Specification { get; }
        public Vector3 Position { get; set; }
        public EntityAnimationEvents AnimationEvents { get; } = new();
        public ReactiveField<bool> IsCanAttack { get; } = new();
        public ReactiveField<bool> IsAttack { get; } = new();
        public ReactiveField<bool> IsSimpleAttack { get; } = new();
        public ReactiveField<bool> InTarget { get; } = new();
        public ReactiveField<float> CurrentSpeed { get; } = new();
        public ReactiveField<IEntityModel> Target { get; } = new(null);
        public ReactiveField<Quaternion> NextRotation { get; } = new();

        public ReactiveField<bool> IsDied { get; } = new();
        
        public ModelCollection<EntityResourceType, EntityResource> Resources { get; } = new();
        
        protected EntityModel(EntitySpecification specification, IEntityModel target)
        {
            Specification = specification;
            Target.Value = target;
            
            Resources.Add(EntityResourceType.Health, new EntityResource(EntityResourceType.Health, specification.MaxHealth));
        }

        protected EntityModel(EntitySpecification specification)
        {
            Specification = specification;
            
            Resources.Add(EntityResourceType.Health, new EntityResource(EntityResourceType.Health, specification.MaxHealth));
        }

        public void TakeDamage(int damage)
        {
            var currentHealth = Resources.GetModel(EntityResourceType.Health);
            currentHealth.Amount.Value -= damage;

            if (currentHealth.Amount.Value <= 0)
            {
                currentHealth.Amount.Value = 0;
                IsDied.Value = true;
            }
        }
    }
}