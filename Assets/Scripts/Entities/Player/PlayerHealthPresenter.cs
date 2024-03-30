using Presenter;

namespace Entities.Player
{
    public class PlayerHealthPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerHealthPresenter(IGameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            // _view.HealthBar.fillAmount = CalculateHealth(100);
            
            // _model.Resources.GetModel(EntityResourceType.Health).Amount.OnChanged += HandleHealthChanged;
        }

        public void Dispose()
        {
            // _model.Resources.GetModel(EntityResourceType.Health).Amount.OnChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int newHealth, int oldHealth)
        {
            _view.HealthBar.fillAmount = CalculateHealth(newHealth);
        }

        private float CalculateHealth(int newHealth)
        {
            return newHealth / 100f;
        }
    }
}