using Cameras;
using Entities.Animation;
using Entities.Player;
using Presenter;
using Skills.SkillPanel;

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
            Presenters.Add(new PlayerPresenter(GameModel, (PlayerModel)GameModel.PlayerModel, _view.PlayerView));
            Presenters.Add(new EntityAnimationPresenter(_view.PlayerView.EntityAnimationEvents, GameModel.PlayerModel.AnimationEvents));

            AfterInit();
            
            Presenters.Init();
            
            GameModel.CameraModel.ChangeState(CameraStateType.PlayerFollow, _view.PlayerView.Root);
        }

        public void Dispose()
        {
            Presenters.Dispose();
            
            AfterDispose();

            GameModel.EnemiesCollection.Clear();
        }

        protected abstract void AfterInit();
        protected abstract void AfterDispose();
    }
}