using Utilities.Model;

namespace Skills.SkillPanel.Slot
{
    public class SkillSlotModel : IModel
    {
        public Skill Skill { get; }

        public SkillSlotModel(Skill skill)
        {
            Skill = skill;
        }
    }
}