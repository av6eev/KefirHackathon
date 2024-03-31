using System.Linq;
using Entities;
using Presenter;
using UnityEngine;

namespace Skills
{
    public class SkillPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly Skill _model;
        private readonly IEntityModel _entityModel;
        
        private GameObject _currentEffect;

        public SkillPresenter(IGameModel gameModel, Skill model, IEntityModel entityModel)
        {
            _gameModel = gameModel;
            _model = model;
            _entityModel = entityModel;
        }

        public void Init()
        {
            _model.StartCastEvent.OnChanged += HandleStartCast;
            
            _entityModel.AnimationEvents.OnEndAttack += HandleEndAttack;
            _entityModel.AnimationEvents.OnNeedEffect += HandleNeedEffect;
        }

        public void Dispose()
        {
            _model.StartCastEvent.OnChanged -= HandleStartCast;
            
            _entityModel.AnimationEvents.OnEndAttack -= HandleEndAttack;
            _entityModel.AnimationEvents.OnNeedEffect -= HandleNeedEffect;
        }

        private void HandleEndAttack()
        {
            if (_model.IsStarted)
            {
                Object.Destroy(_currentEffect);
                
                _model.IsStarted = false;
                _entityModel.IsAttack.Value = false;
            }
        }

        private void HandleNeedEffect()
        {
            if (_model.IsStarted)
            {
                _currentEffect = Object.Instantiate(_model.Specification.Effect, _entityModel.Position, Quaternion.identity);
            }
        }

        private void HandleStartCast()
        {
            TrySetTargetInCircle();
        }

        private void TrySetTargetInCircle()
        {
            foreach (var entity in _gameModel.EnemiesCollection.GetModels().Where(IsNearToPlayer))
            {
                entity.TakeDamage(_model.Specification.Damage);
            }
        }

        private bool IsNearToPlayer(EntityModel enemy) => GetDistanceToPlayer(enemy) <= _model.Specification.Distance;
        private float GetDistanceToPlayer(EntityModel enemy) => Vector3.Distance(enemy.Position, _gameModel.PlayerModel.Position);
    }
}