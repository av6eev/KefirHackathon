using System;
using Specification;
using UnityEngine;

namespace Skills.Specification
{
    [Serializable]
    public class MeleeSkillSpecification : BaseSpecification
    {
        public float Cooldown;
        public Sprite Icon;
        public float Angle;
        public float Distance;
        public int Damage;
        public GameObject Effect;
    }
}