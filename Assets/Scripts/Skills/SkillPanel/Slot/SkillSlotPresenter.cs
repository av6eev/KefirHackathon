using Presenter;

namespace Skills.SkillPanel.Slot
{
    public class SkillSlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SkillSlotModel _model;
        private readonly SkillSlotView _view;

        private IPresenter _skillPresenter;
        
        public SkillSlotPresenter(IGameModel gameModel, SkillSlotModel model, SkillSlotView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.Icon.sprite = _model.Skill.Specification.Icon;
            
            _skillPresenter = new SkillPresenter(_gameModel, _model.Skill);
            _skillPresenter.Init();
        }

        public void Dispose()
        {
            _skillPresenter.Dispose();
        }
    }
}