namespace GameScenes.GameUI
{
    public class GameUiScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameUiSceneView _view;

        public GameUiScenePresenter(GameModel gameModel, GameUiSceneView view) : base(gameModel, view)
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