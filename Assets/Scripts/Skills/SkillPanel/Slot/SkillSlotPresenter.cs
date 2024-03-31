using Entities;
using Entities.Player;
using Presenter;

namespace Skills.SkillPanel.Slot
{
    public class SkillSlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SkillSlotModel _model;
        private readonly SkillSlotView _view;
        private readonly IEntityModel _entityModel;

        private IPresenter _skillPresenter;
        
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
            
            _skillPresenter = new SkillPresenter(_gameModel, _model.Skill, _entityModel);
            _skillPresenter.Init();
        }

        public void Dispose()
        {
            _skillPresenter.Dispose();
        }
    }
}