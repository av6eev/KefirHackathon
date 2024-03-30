namespace GameScenes.Hub
{
    public class HubScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly HubSceneView _view;

        public HubScenePresenter(GameModel gameModel, HubSceneView view) : base(gameModel, view)
        {
            _gameModel = gameModel;
            _view = view;
        }
        
        protected override void AfterInit()
        {
        }

        protected override void AfterDispose()
        {
        }
    }
}