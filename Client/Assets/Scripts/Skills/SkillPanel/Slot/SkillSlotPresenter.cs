using Entities;
using Entities.Player;
using Presenter;
using Updater;

namespace Skills.SkillPanel.Slot
{
    public class SkillSlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SkillSlotModel _model;
        private readonly SkillSlotView _view;
        private readonly IEntityModel _entityModel;

        private IPresenter _skillPresenter;
        private IUpdater _updater;

        public SkillSlotPresenter(IGameModel gameModel, SkillSlotModel model, SkillSlotView view, IEntityModel entityModel)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
            _entityModel = entityModel;
        }
        
        public void Init()
        {
            _view.Icon.sprite = _model.Skill.Specification.Icon;
            
            _updater = new SkillCooldownUpdater(_model, _view);
            _gameModel.UpdatersList.Add(_updater);
            
            _skillPresenter = new SkillPresenter(_gameModel, _model.Skill, _entityModel);
            _skillPresenter.Init();

            _model.Skill.IsCooldown.OnChanged += HandleCooldownState;
        }

        public void Dispose()
        {
            _gameModel.UpdatersList.Remove(_updater);

            _skillPresenter.Dispose();
            
            _model.Skill.IsCooldown.OnChanged -= HandleCooldownState;
        }

        private void HandleCooldownState(bool newValue, bool oldValue)
        {
            _view.CooldownRoot.SetActive(newValue);
        }
    }
}