using DeBuff;
using Presenter;

namespace GameScenes.GameUI.DeBuffPanel
{
    public class DeBuffPenaltyPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;

        public DeBuffPenaltyPresenter(IGameModel gameModel)
        {
            _gameModel = gameModel;
        }
        
        public void Init()
        {
            _gameModel.DeBuffsCollection.GetModel(DeBuffType.InverseInput).IsActive.OnChanged += HandleInverseInput;
            _gameModel.DeBuffsCollection.GetModel(DeBuffType.Breath).IsActive.OnChanged += HandleBreath;
        }

        public void Dispose()
        {
            _gameModel.DeBuffsCollection.GetModel(DeBuffType.InverseInput).IsActive.OnChanged -= HandleInverseInput;
            _gameModel.DeBuffsCollection.GetModel(DeBuffType.Breath).IsActive.OnChanged -= HandleBreath;
        }

        private void HandleBreath(bool newValue, bool oldValue)
        {
            if (newValue)
            {
                var specification = _gameModel.DeBuffsCollection.GetModel(DeBuffType.Breath).Specification;
                
                _gameModel.PlayerDialogModel.Add(specification.DialogText);

                _gameModel.PlayerModel.Death();
            }
        }

        private void HandleInverseInput(bool newValue, bool oldValue)
        {
            if (newValue)
            {
                var specification = _gameModel.DeBuffsCollection.GetModel(DeBuffType.InverseInput).Specification;

                _gameModel.PlayerDialogModel.Add(specification.DialogText);
                _gameModel.PlayerModel.InverseInput(true);
            }
            else
            {
                _gameModel.PlayerModel.InverseInput(false);
            }
        }
    }
}