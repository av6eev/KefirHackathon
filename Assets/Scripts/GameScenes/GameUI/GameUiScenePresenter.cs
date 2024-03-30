using Dialogs;
using Dialogs.Collection;
using Entities.Player;
using Item.ItemPlaceholder;
using PlayerInventory.Hud;
using Presenter;

namespace GameScenes.GameUI
{
    public class GameUiScenePresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameUiSceneView _view;
        
        private PlayerInventoryHudModel _hudInventoryModel;
        private readonly PresentersList _presenters = new();

        public GameUiScenePresenter(GameModel gameModel, GameUiSceneView view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        public void Init()
        {
            var hudModel = _gameModel.InventoriesCollection.GetModel(PlayerModel.HudId);
            
            _gameModel.ItemPlaceholderModel = new ItemPlaceholderModel();
            _gameModel.DialogsCollection = new DialogsCollection();

            _hudInventoryModel = new PlayerInventoryHudModel(hudModel);
            
            _presenters.Add(new DialogsCollectionPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection, _view.DialogsCollectionView));
            _presenters.Add(new PlayerInventoryHudPresenter(_gameModel, _hudInventoryModel, _view.PlayerInventoryHudView));
            _presenters.Add(new EscapeDialogPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection));
            _presenters.Add(new ItemPlaceholderPresenter(_gameModel, (ItemPlaceholderModel) _gameModel.ItemPlaceholderModel, _view.ItemPlaceholderView));
            
            _presenters.Init();
        }

        public void Dispose()
        {
            _presenters.Dispose();
        }
    }
}