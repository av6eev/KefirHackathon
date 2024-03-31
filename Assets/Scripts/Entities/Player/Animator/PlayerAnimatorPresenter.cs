using Presenter;

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
            
            _gameModel.InputModel.IsRun.OnChanged -= HandleRun;
            _gameModel.InputModel.OnAttack -= HandleAttackInput;
            
            _view.EntityAnimationEvents.OnEndSimpleAttack -= HandleEndSimpleAttack;
            _view.EntityAnimationEvents.OnDeath -= HandleViewDeath;
        }

        private void HandleViewDeath()
        {
            _gameModel.SceneManagementModelsCollection.Unload("arena_scene");
            _gameModel.SceneManagementModelsCollection.Load("hub_scene");

            _gameModel.Rerun = true;
        }

        private void HandleDeath()
        {
            _view.EntityAnimatorController.SetBool("IsMovement", false);
            _view.EntityAnimatorController.SetTrigger("IsDeath");
        }

        private void HandleAttackInput()
        {
            if (!_model.InDash.Value && !_gameModel.SkillPanelModel.IsCasting)
            {
                _model.IsSimpleAttack.Value = true;
                _view.EntityAnimatorController.SetTrigger("SimpleAttack");
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
            }
            else
            {
                _view.DisableIdleAnimation();
            }
            
            var normalizedSpeed = _model.CurrentSpeed.Value / _model.Specification.RunSpeed;

            if (normalizedSpeed < .5f && .5f - normalizedSpeed < .01f)
            {
                normalizedSpeed = 0.49f;
            }
            
            _view.SetAnimationMovementSpeed(normalizedSpeed);
        }

        private void HandleRun(bool newState, bool oldState)
        {
            _model.IsRunning = newState;
        }
    }
}