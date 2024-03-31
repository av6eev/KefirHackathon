using GameScenes.GameUI;
using Presenter;
using UnityEngine.UI;

namespace Entities.Player
{
    public class PlayerHealthPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly IPlayerModel _model;
        private readonly PlayerMainResourceView _view;

        public PlayerHealthPresenter(IGameModel gameModel, IPlayerModel model, PlayerMainResourceView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            var healthResource = _model.Resources.GetModel(EntityResourceType.Health);
            
            _view.FillBar.fillAmount = CalculateHealth(healthResource.Amount.Value);
            _view.PercentageText.text = $"{healthResource.Amount.Value}%";

            healthResource.Amount.OnChanged += HandleHealthChanged;
        }

        public void Dispose()
        {
            _model.Resources.GetModel(EntityResourceType.Health).Amount.OnChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int newHealth, int oldHealth)
        {
            _view.FillBar.fillAmount = CalculateHealth(newHealth);
            _view.PercentageText.text = $"{newHealth}%";
        }

        private float CalculateHealth(int newHealth)
        {
            return newHealth / 100f;
        }
    }
}