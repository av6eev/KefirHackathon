using Presenter;
using Skills.SkillPanel.Slot;

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
            var isCooldown = _model.GetModel(index).Skill.IsCooldown.Value;
            
            if (isCooldown || _model.IsCasting) return;
            
            _model.CurrentSkillIndex = index;
            _gameModel.PlayerModel.IsAttack.Value = true;

            _model.GetModel(index).Skill.StartCast();
            _model.IsCasting = true;
        }
    }
}