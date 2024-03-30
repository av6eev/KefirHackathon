using Presenter;

namespace Entities.Enemy
{
    public class EnemyHealthPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyHealthPresenter(IGameModel gameModel, EnemyModel model, EnemyView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.HealthBar.fillAmount = CalculateHealth(_model.Specification.MaxHealth);
            
            _model.CurrentHealth.OnChanged += HandleHealthChanged;
        }

        public void Dispose()
        {
            _model.CurrentHealth.OnChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int newHealth, int oldHealth)
        {
            _view.HealthBar.fillAmount = CalculateHealth(newHealth);
        }

        private int CalculateHealth(int newHealth)
        {
            return newHealth / _model.Specification.MaxHealth;
        }
    }
}