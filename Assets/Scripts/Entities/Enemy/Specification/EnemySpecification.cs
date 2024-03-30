using System;
using Specification;

namespace Entities.Enemy.Specification
{
    [Serializable]
    public class EnemySpecification : BaseSpecification
    {
        public string PrefabId;
        public float PatrolSpeed;
        public float MoveTowardsTargetSpeed;
        public float RotationSpeed;
        public float AttackRange;
        public float ObserveRange;
        public float PatrolTime;
        public float IdleTime;
    }
}