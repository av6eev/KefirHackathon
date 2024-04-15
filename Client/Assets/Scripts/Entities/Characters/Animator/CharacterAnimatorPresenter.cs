using System;
using Entities.Player;
using Presenter;
using ServerCore.Main.Utilities;
using UnityEngine;

namespace Entities.Characters.Animator
{
    public class CharacterAnimatorPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly CharacterModel _model;
        private readonly PlayerView _playerView;

        public CharacterAnimatorPresenter(IGameModel gameModel, CharacterModel model, PlayerView playerView)
        {
            _gameModel = gameModel;
            _model = model;
            _playerView = playerView;
        }
        
        public void Init()
        {
            _model.ServerData.AnimationState.Changed += HandleAnimationStateChange;
            _model.ServerData.Speed.Changed += HandleSpeedChange;
        }

        public void Dispose()
        {
            _model.ServerData.AnimationState.Changed -= HandleAnimationStateChange;
            _model.ServerData.Speed.Changed -= HandleSpeedChange;
        }

        private void HandleSpeedChange()
        {
            _model.CurrentSpeed.Value = _model.ServerData.Speed.Value;
            
            if (_model.CurrentSpeed.Value == 0)
            {
                _playerView.SetAnimationMovementSpeed(0);
                _playerView.EnableIdleAnimation();
                
                _model.AnimationState.Value = EntityAnimationState.Idle;
            }
            else
            {
                _playerView.DisableIdleAnimation();
            }
            
            var normalizedSpeed = _model.CurrentSpeed.Value / _model.Specification.RunSpeed;

            if (normalizedSpeed < .5f && .5f - normalizedSpeed < .01f)
            {
                normalizedSpeed = 0.49f;
                _model.AnimationState.Value = EntityAnimationState.Walk;
            }
            
            _playerView.SetAnimationMovementSpeed(normalizedSpeed);
        }

        private void HandleAnimationStateChange()
        {
            var newValue = (EntityAnimationState)_model.ServerData.AnimationState.Value;
            
            switch (newValue)
            {
                case EntityAnimationState.Dash:
                    _playerView.EntityAnimatorController.SetTrigger("IsRoll");
                    break;
                case EntityAnimationState.Death:
                    _playerView.EntityAnimatorController.SetBool("IsMovement", false);
                    _playerView.EntityAnimatorController.SetTrigger("IsDeath");
                    break;
                case EntityAnimationState.Attack:
                    _playerView.EntityAnimatorController.SetTrigger("IsAttack");
                    break;
                case EntityAnimationState.SimpleAttack:
                    _playerView.EntityAnimatorController.SetTrigger("SimpleAttack");
                    break;
            }
        }
    }
}