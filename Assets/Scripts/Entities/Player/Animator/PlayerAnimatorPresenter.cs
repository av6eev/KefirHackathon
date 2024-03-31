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

            _gameModel.InputModel.IsRun.OnChanged += HandleRun;
        }

        public void Dispose()
        {
            _model.CurrentSpeed.OnChanged -= ChangeCurrentAnimation;
            _model.IsAttack.OnChanged -= HandleAttackState;
            
            _gameModel.InputModel.IsRun.OnChanged -= HandleRun;
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