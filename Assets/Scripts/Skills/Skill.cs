using Entities;
using Reactive.Event;
using Skills.Specification;

namespace Skills
{
    public class Skill
    {
        public EntityModel Owner { get; }
        public ReactiveEvent StartCastEvent = new();
        
        public readonly MeleeSkillSpecification Specification;
        public bool IsCoolDown { get; private set; }

        public Skill(MeleeSkillSpecification specification, EntityModel owner)
        {
            Owner = owner;
            Specification = specification;
        }

        public void StartCast()
        {
            IsCoolDown = true;
            StartCastEvent.Invoke();
        }
    }
}