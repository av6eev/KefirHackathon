using Presenter;

namespace Entities.Player.Animator
{
    public class PlayerDashPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerDashPresenter(IGameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }

        public void Init()
        {
            _gameModel.InputModel.OnDash += HandleDash;
            _view.EntityAnimationEvents.OnEndRoll += HandleEndRoll;
        }

        public void Dispose()
        {
            _gameModel.InputModel.OnDash -= HandleDash;
            _view.EntityAnimationEvents.OnEndRoll -= HandleEndRoll;
        }

        private void HandleEndRoll()
        {
            _model.IsDashing.Value = false;
        }

        private void HandleDash()
        {
            _model.IsDashing.Value = true;
            _view.EntityAnimatorController.SetTrigger("IsRoll");
        }
    }
}