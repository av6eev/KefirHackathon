using Entities;
using Reactive.Event;
using Reactive.Field;
using Skills.Specification;

namespace Skills
{
    public class Skill
    {
        public EntityModel Owner { get; }
        public ReactiveEvent StartCastEvent = new();
        
        public readonly MeleeSkillSpecification Specification;
        public float Cooldown { get; set; }
        public ReactiveField<bool> IsCooldown { get; } = new();
        public bool IsStarted { get; set; }

        public Skill(MeleeSkillSpecification specification, EntityModel owner)
        {
            Owner = owner;
            Specification = specification;
        }

        public void StartCast()
        {
            IsCooldown.Value = true;
            Cooldown = 0;
            IsStarted = true;
            
            StartCastEvent.Invoke();
        }
    }
}