using DeBuff.Specification;
using Presenter;
using Skills.SkillPanel.Slot;
using UnityEngine;

namespace Skills.SkillPanel
{
    public class SkillPanelPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SkillPanelModel _model;
        private readonly SkillPanelView _view;

        private readonly PresentersList _presenters = new();
        
        public SkillPanelPresenter(IGameModel gameModel, SkillPanelModel model, SkillPanelView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            for (var index = 0; index < _model.Count; index++)
            {
                var presenter = new SkillSlotPresenter(_gameModel, _model.GetModel(index), _view.SlotViews[index], _gameModel.PlayerModel);
                _presenters.Add(presenter);
            }
            
            _presenters.Init();
            
            _gameModel.InputModel.OnSkillUse += HandleSkillUse;
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
            
            _gameModel.InputModel.OnSkillUse -= HandleSkillUse;
        }

        private void HandleSkillUse(int index)
        {
            var skill = _model.GetModel(index).Skill;
            var isCooldown = skill.IsCooldown.Value;
            
            if (isCooldown || _model.IsCasting || _gameModel.PlayerModel.InDash.Value) return;

            var deBuffModel = _gameModel.DeBuffsCollection.GetModel(DeBuffType.ForgotChance);
            
            if (deBuffModel.IsActive.Value)
            {
                var chance = 100 - deBuffModel.Specification.Chance;
                
                if (chance < Random.Range(0, 100))
                {
                    _gameModel.PlayerDialogModel.Add(deBuffModel.Specification.DialogText);
                    skill.Cooldown = 0;
                    skill.IsCooldown.Value = true;

                    return;
                }
            }
            
            _model.CurrentSkillIndex = index;
            _gameModel.PlayerModel.IsAttack.Value = true;

            skill.StartCast();
            _model.IsCasting = true;
        }
    }
}