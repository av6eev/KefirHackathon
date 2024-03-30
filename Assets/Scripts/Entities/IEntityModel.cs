using Entities.Specification;
using Reactive.Field;
using UnityEngine;

namespace Entities
{
    public interface IEntityModel
    {
        EntitySpecification Specification { get; }
        Vector3 Position { get; }
        ReactiveField<bool> IsCanAttack { get; } 
        ReactiveField<float> CurrentSpeed { get; }
        ReactiveField<int> CurrentHealth { get; }
        ReactiveField<IEntityModel> Target { get; }
        ReactiveField<bool> InTarget { get; }
        ReactiveField<Quaternion> NextRotation { get; }
    }
}