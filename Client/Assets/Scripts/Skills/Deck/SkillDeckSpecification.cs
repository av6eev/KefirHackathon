using System;
using System.Collections.Generic;
using Skills.Specification;
using Specification;

namespace Skills.Deck
{
    [Serializable]
    public class SkillDeckSpecification : BaseSpecification
    {
        public List<MeleeSkillSpecification> MeleeSkills;
    }
}