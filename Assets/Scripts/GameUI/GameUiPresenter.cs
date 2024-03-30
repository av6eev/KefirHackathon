using Presenter;

namespace GameUI
{
    public class GameUiPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameUiSceneView _view;

        public GameUiPresenter(GameModel gameModel, GameUiSceneView view)
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