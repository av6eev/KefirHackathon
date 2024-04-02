using System;
using Entities.Specification;

namespace Entities.Enemy.Specification
{
    [Serializable]
    public class EnemySpecification : EntitySpecification
    {
        public string PrefabId;
        public float PatrolSpeed;
        public float MoveTowardsTargetSpeed;
        public float AttackRange;
        public float ObserveRange;
        public float IdleTime;
        public FireBallView CastGameObjectPrefabId;
    }
}