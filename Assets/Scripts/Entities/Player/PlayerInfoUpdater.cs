using UnityEngine;
using Updater;

namespace Entities.Player
{
    public class PlayerInfoUpdater : IUpdater
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;

        public PlayerInfoUpdater(PlayerModel playerModel, PlayerView playerView)
        {
            _playerModel = playerModel;
            _playerView = playerView;
        }
        
        public void Update(float deltaTime)
        {
            _playerModel.Position = _playerView.Position;

            if (_playerModel.Target.Value == null) return;

            if (Vector3.Distance(_playerModel.Position, _playerModel.Target.Value.Position) < _playerModel.Specification.AttackDistance)
            {
                _playerModel.IsCanAttack.Value = true;
            }
            else
            {
                _playerModel.IsCanAttack.Value = false;
            }
        }
    }
}