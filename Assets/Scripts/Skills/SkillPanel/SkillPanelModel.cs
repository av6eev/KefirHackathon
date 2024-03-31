using Entities.Player;
using Skills.Deck;
using Skills.SkillPanel.Slot;
using Utilities.ModelCollection;

namespace Skills.SkillPanel
{
    public class SkillPanelModel : ModelCollection<SkillSlotModel>
    {
        public int CurrentSkillIndex { get; set; }

        public SkillPanelModel(SkillDeckSpecification deckSpecification, PlayerModel owner)
        {
            foreach (var meleeSkillSpecification in deckSpecification.MeleeSkills)
            {
                Add(new SkillSlotModel(new Skill(meleeSkillSpecification, owner)));
            }
        }
    }
}