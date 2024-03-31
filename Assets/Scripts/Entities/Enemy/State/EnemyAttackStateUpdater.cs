using UnityEngine;
using Updater;

namespace Entities.Enemy.State
{
    public class EnemyAttackStateUpdater : IUpdater
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly GameObject _castedGo;

        public EnemyAttackStateUpdater(EnemyModel model, EnemyView view, GameObject castedGo)
        {
            _model = model;
            _view = view;
            _castedGo = castedGo;
        }

        public void Update(float deltaTime)
        {
            if (!_model.IsAttack.Value) return;
        }
    }
}