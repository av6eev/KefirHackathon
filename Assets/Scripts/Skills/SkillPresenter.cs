using System.Linq;
using Entities.Enemy;
using Presenter;
using UnityEngine;

namespace Skills
{
    public class SkillPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly Skill _model;

        public SkillPresenter(IGameModel gameModel, Skill model)
        {
            _gameModel = gameModel;
            _model = model;
        }

        public void Init()
        {
            _model.StartCastEvent.OnChanged += HandleStartCast;
        }

        public void Dispose()
        {
            _model.StartCastEvent.OnChanged -= HandleStartCast;
        }

        private void HandleStartCast()
        {
            TrySetTargetInCircle();
            Debug.Log($"skill cast {_model.Specification.Icon.name}");
        }

        private void TrySetTargetInCircle()
        {
            foreach (var entity in _gameModel.EnemiesCollection.GetModels().Where(IsNearToPlayer))
            {
                Debug.Log(entity.Specification.Id);
            }
        }

        private bool IsNearToPlayer(EnemyModel enemy) => GetDistanceToPlayer(enemy) <= _model.Specification.Distance;
        private float GetDistanceToPlayer(EnemyModel enemy) => Vector3.Distance(enemy.Position, _gameModel.PlayerModel.Position);
    }
}