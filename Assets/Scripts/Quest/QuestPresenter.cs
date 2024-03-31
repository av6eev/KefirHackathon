using Presenter;
using Quest.Demands.Specification.KillEnemy;
using Object = UnityEngine.Object;

namespace Quest
{
    public class QuestPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly QuestModel _model;
        private readonly QuestView _view;
        
        private KillEnemyQuestPresenter _presenter;

        public QuestPresenter(IGameModel gameModel, QuestModel model, QuestView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.Title.text = _model.Specification.Title;
            _view.Description.text = _model.Specification.Description;

            var demandId = _model.Specification.DemandId;
            var specification = _gameModel.Specifications.DemandSpecifications[demandId];

            switch (specification)
            {
                case KillEnemyDemandSpecification killEnemyDemand:
                    _presenter = new KillEnemyQuestPresenter(_gameModel, _model, _view, killEnemyDemand);
                    break;
            }
            
            _presenter.Init();
        }

        public void Dispose()
        {
            Object.Destroy(_view.gameObject);
            
            _presenter.Dispose();
        }
    }
}