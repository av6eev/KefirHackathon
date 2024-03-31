using Presenter;
using UnityEngine;

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
            
            _model.Resources.GetModel(EntityResourceType.Health).Amount.OnChanged += HandleHealthChanged;
        }

        public void Dispose()
        {
            _model.Resources.GetModel(EntityResourceType.Health).Amount.OnChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(float newHealth, float oldHealth)
        {
            _view.HealthBar.fillAmount = CalculateHealth(newHealth);
        }

        private float CalculateHealth(float newHealth)
        {
            return newHealth / _model.Specification.MaxHealth;
        }
    }
}