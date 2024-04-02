using Updater;

namespace Skills.SkillPanel.Slot
{
    public class SkillCooldownUpdater : IUpdater
    {
        private readonly SkillSlotModel _model;
        private readonly SkillSlotView _view;

        public SkillCooldownUpdater(SkillSlotModel model, SkillSlotView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            var skill = _model.Skill;
            
            if (!skill.IsCooldown.Value) return;

            if (skill.Cooldown < skill.Specification.Cooldown)
            {
                skill.Cooldown += deltaTime;
                _view.CooldownFillBar.fillAmount = 1 - skill.Cooldown / skill.Specification.Cooldown;
            }
            else
            {
                skill.IsCooldown.Value = false;
            }
        }
    }
}