﻿using Presenter;
using ServerCore.Main.Commands;
using ServerCore.Main.Utilities;
using UnityEngine;

namespace Entities.Player.Animator
{
    public class PlayerAnimatorPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerAnimatorPresenter(GameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _model.CurrentSpeed.OnChanged += ChangeCurrentAnimation;
            _model.IsAttack.OnChanged += HandleAttackState;
            _model.DeathEvent.OnChanged += HandleDeath;
            _model.AnimationState.OnChanged += HandleAnimationStateChange;
            
            _gameModel.InputModel.IsRun.OnChanged += HandleRun;
            _gameModel.InputModel.OnAttack += HandleAttackInput;

            _view.EntityAnimationEvents.OnEndSimpleAttack += HandleEndSimpleAttack;
            _view.EntityAnimationEvents.OnDeath += HandleViewDeath;
        }

        public void Dispose()
        {
            _model.CurrentSpeed.OnChanged -= ChangeCurrentAnimation;
            _model.IsAttack.OnChanged -= HandleAttackState;
            _model.DeathEvent.OnChanged -= HandleDeath;
            _model.AnimationState.OnChanged -= HandleAnimationStateChange;
            
            _gameModel.InputModel.IsRun.OnChanged -= HandleRun;
            _gameModel.InputModel.OnAttack -= HandleAttackInput;
            
            _view.EntityAnimationEvents.OnEndSimpleAttack -= HandleEndSimpleAttack;
            _view.EntityAnimationEvents.OnDeath -= HandleViewDeath;
        }

        private void HandleAnimationStateChange(EntityAnimationState newState, EntityAnimationState oldValue)
        {
            var command = new EntityAnimationCommand(_model.Id, newState);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
        }

        private void HandleViewDeath()
        {
            _gameModel.SceneManagementModelsCollection.Unload("arena_scene");
            _gameModel.SceneManagementModelsCollection.Load("hub_scene");

            _gameModel.Rerun = true;
            
            var amnesiaResource = _model.Resources.GetModel(EntityResourceType.Amnesia);
            amnesiaResource.Amount.Value = amnesiaResource.MinAmount;
            
            var healthResource = _model.Resources.GetModel(EntityResourceType.Amnesia);
            healthResource.Amount.Value = amnesiaResource.MaxAmount;
            
            _model.InverseInput(false);
        }

        private void HandleDeath()
        {
            _view.EntityAnimatorController.SetBool("IsMovement", false);
            _view.EntityAnimatorController.SetTrigger("IsDeath");
            
            _model.AnimationState.Value = EntityAnimationState.Death;
        }

        private void HandleAttackInput()
        {
            if (!_model.InDash.Value && !_gameModel.SkillPanelModel.IsCasting)
            {
                _model.IsSimpleAttack.Value = true;
                _view.EntityAnimatorController.SetTrigger("SimpleAttack");
                
                _model.AnimationState.Value = EntityAnimationState.SimpleAttack;
            }
        }

        private void HandleEndSimpleAttack()
        {
            if (_model.Target.Value != null)
            {
                _model.Target.Value.TakeDamage(_model.Specification.BaseDamage);
            }
            
            _model.IsSimpleAttack.Value = false;
        }

        private void HandleAttackState(bool newValue, bool oldValue)
        {
            if (newValue)
            {
                _view.EntityAnimatorController.SetTrigger("IsAttack");
                _view.EntityAnimatorController.SetInteger("AttackType", _gameModel.SkillPanelModel.CurrentSkillIndex);

                _model.AnimationState.Value = EntityAnimationState.Attack;
            }
        }

        private void ChangeCurrentAnimation(float newSpeed, float oldSpeed)
        {
            if (_view == null)
            {
                return;
            }
            
            if (newSpeed == 0)
            {
                _view.SetAnimationMovementSpeed(0);
                _view.EnableIdleAnimation();
                
                _model.AnimationState.Value = EntityAnimationState.Idle;
            }
            else
            {
                _view.DisableIdleAnimation();
            }
            
            var normalizedSpeed = _model.CurrentSpeed.Value / _model.Specification.RunSpeed;

            if (normalizedSpeed < .5f && .5f - normalizedSpeed < .01f)
            {
                normalizedSpeed = 0.49f;
                _model.AnimationState.Value = EntityAnimationState.Walk;
            }
            
            _view.SetAnimationMovementSpeed(normalizedSpeed);
        }

        private void HandleRun(bool newState, bool oldState)
        {
            _model.IsRunning = newState;
            _model.AnimationState.Value = EntityAnimationState.Run;
        }
    }
}