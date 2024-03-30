using Presenter;

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
            AfterInit();
            
            Presenters.Init();
        }

        public void Dispose()
        {
            AfterDispose();
            
            Presenters.Dispose();
        }

        protected abstract void AfterInit();
        protected abstract void AfterDispose();
    }
}