using System;
using Specification;

namespace Entities.Specification
{
    [Serializable]
    public class EntitySpecification : BaseSpecification
    {
        public float WalkSpeed = 4f;
        public float RunSpeed = 4f * 1.4f;
        public float DashSpeed = 10f;
        public float RotationSpeed = 6f;
        public float AttackDistance;
        public int MaxHealth;
    }
}