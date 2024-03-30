using Skills.Deck;
using Skills.SkillPanel.Slot;
using Utilities.ModelCollection;

namespace Skills.SkillPanel
{
    public class SkillPanelModel : ModelCollection<SkillSlotModel>
    {
        public SkillPanelModel(SkillDeckSpecification deckSpecification)
        {
            foreach (var meleeSkillSpecification in deckSpecification.MeleeSkills)
            {
                Add(new SkillSlotModel(new Skill(meleeSkillSpecification)));
            }
        }
    }
}