using System.Linq;
using Entities.Enemy;
using UnityEngine;
using Updater;

namespace Entities.Player.Target
{
    public class PlayerTargetUpdater : IUpdater
    {
        private const float AngleVisionDistance = 3f;
        private const float AngleVision = 70f;
        private const float CircleVisionDistance = 3f;
        
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        
        public PlayerTargetUpdater(IGameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            if (!TrySetTargetInAngle() && !TrySetTargetInCircle())
            {
                SetEnemy(null);
            }
        }

        private bool IsNearToPlayer(EnemyModel enemy) => GetDistanceToPlayer(enemy) <= CircleVisionDistance;
        private float GetDistanceToPlayer(EnemyModel enemy) => Vector3.Distance(enemy.Position, _model.Position);
        private bool IsChangeTarget(EnemyModel enemy) => _model.Target.Value == null || _model.Target.Value != enemy;
        private void SetEnemy(EnemyModel enemy) => _model.Target.Value = enemy;
        
        private bool TrySetTargetInCircle()
        {
            foreach (var entity in _gameModel.EnemiesCollection.GetModels().Where(IsNearToPlayer).OrderBy(GetDistanceToPlayer))
            {
                if (!RaycastHelper.IsRaycastTarget(_model.Position,entity.Position, out var hit, AngleVisionDistance, _view.EnemyLayer)) continue;
                
                if (IsChangeTarget(entity))
                {
                    SetEnemy(entity);
                }

                return true;
            }

            return false;
        }

        private bool TrySetTargetInAngle()
        {
            foreach (var entity in _gameModel.EnemiesCollection.GetModels().Where(entityModel => GetDistanceToPlayer(entityModel) <= AngleVisionDistance).OrderBy(GetDistanceToPlayer))
            {
                if (!IsEntityInVision(entity) || !RaycastHelper.IsRaycastTarget(_model.Position,entity.Position, out var hit, AngleVisionDistance, _view.EnemyLayer)) continue;
                
                if (IsChangeTarget(entity))
                {
                    SetEnemy(entity);
                }

                return true;
            }

            return false;
        }

        private bool IsEntityInVision(EnemyModel enemy)
        {
            var deltaVector = enemy.Position - _model.Position;
            var look = Quaternion.LookRotation(deltaVector);

            var direction = _view.Forward;
            var directionRotation = direction == Vector3.zero ? _view.Rotation : Quaternion.LookRotation(direction);
            var angleBetween = Quaternion.Angle(directionRotation, look);

            return angleBetween <= AngleVision;
        }
    }
}