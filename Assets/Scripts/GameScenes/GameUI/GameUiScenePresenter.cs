using Presenter;

namespace GameScenes.GameUI
{
    public class GameUiScenePresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameUiSceneView _view;

        public GameUiScenePresenter(GameModel gameModel, GameUiSceneView view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        public void Init()
        {
        }

        public void Dispose()
        {
        }
    }
}