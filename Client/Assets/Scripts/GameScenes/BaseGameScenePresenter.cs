using Cameras;
using Entities.Animation;
using Entities.Player;
using Entities.Player.Dialog;
using Presenter;
using Quest.Collection;

namespace GameScenes
{
    public abstract class BaseGameScenePresenter : IPresenter
    {
        protected readonly GameModel GameModel;
        private readonly BaseGameSceneView _view;

        protected readonly PresentersList Presenters = new();
        
        protected BaseGameScenePresenter(GameModel gameModel, BaseGameSceneView view)
        {
            GameModel = gameModel;
            _view = view;
        }
        
        public void Init()
        {
            Presenters.Add(new CameraPresenter(GameModel, (CameraModel)GameModel.CameraModel, _view.CameraView));
            Presenters.Add(new PlayerPresenter(GameModel, (PlayerModel)GameModel.PlayerModel, null));
            // Presenters.Add(new PlayerDialogPresenter(GameModel, GameModel.PlayerDialogModel, _view.PlayerView.DialogView));
            // Presenters.Add(new EntityAnimationPresenter(_view.PlayerView.EntityAnimationEvents, GameModel.PlayerModel.AnimationEvents));
            
            AfterInit();
            
            Presenters.Init();
        }

        public void Dispose()
        {
            Presenters.Dispose();
            
            AfterDispose();

            GameModel.EnemiesCollection.Clear();
            GameModel.QuestsCollection.Clear();
        }

        protected abstract void AfterInit();
        protected abstract void AfterDispose();
    }
}