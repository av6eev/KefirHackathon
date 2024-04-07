using Entities.Animation;
using Entities.Specification;
using Reactive.Event;
using Reactive.Field;
using UnityEngine;

namespace Entities
{
    public interface IEntityModel
    {
        EntitySpecification Specification { get; }
        Vector3 Position { get; }
        EntityAnimationEvents AnimationEvents { get; }
        ReactiveField<bool> IsCanAttack { get; } 
        ReactiveField<bool> IsAttack { get; } 
        ReactiveField<bool> IsSimpleAttack { get; }
        ReactiveField<float> CurrentSpeed { get; }
        ReactiveField<IEntityModel> Target { get; }
        ReactiveField<bool> InTarget { get; }
        ReactiveField<Quaternion> NextRotation { get; }
        ReactiveField<bool> IsDied { get; }
        void TakeDamage(int damage);
    }
}