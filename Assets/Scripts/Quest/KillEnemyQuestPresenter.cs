using Presenter;
using Quest.Demands.Specification.KillEnemy;

namespace Quest
{
    public class KillEnemyQuestPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly QuestModel _model;
        private readonly QuestView _view;
        private readonly KillEnemyDemandSpecification _killEnemyDemand;

        public KillEnemyQuestPresenter(IGameModel gameModel, QuestModel model, QuestView view, KillEnemyDemandSpecification killEnemyDemand)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
            _killEnemyDemand = killEnemyDemand;
        }

        public void Init()
        {
            _view.Description.text = $"Kill enemy {0}/{_killEnemyDemand.KillCount}";

            _gameModel.PlayerModel.KillCount.OnChanged += HandleKill;
        }

        public void Dispose()
        {
            _gameModel.PlayerModel.KillCount.OnChanged -= HandleKill;
        }

        private void HandleKill(int newValue, int oldValue)
        {
            if (_killEnemyDemand.Verify(_gameModel))
            {
                foreach (var rewardId in _model.Specification.RewardIds)
                {
                    var reward = _gameModel.Specifications.RewardSpecifications[rewardId];
                    reward.Give(_gameModel);
                }

                _gameModel.PlayerModel.KillCount.Value -= _killEnemyDemand.KillCount;
                _gameModel.QuestsCollection.Remove(_model);
            }
            else
            {
                _view.Description.text = $"Kill enemy {newValue}/{_killEnemyDemand.KillCount}";
            }
        }
    }
}