using DeBuff.Collection;
using Dialogs;
using Dialogs.Collection;
using Entities.Player;
using GameScenes.GameUI.DeBuffPanel;
using Item.ItemPlaceholder;
using Presenter;
using Skills.SkillPanel;

namespace GameScenes.GameUI
{
    public class GameUiScenePresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameUiSceneView _view;
        
        private readonly PresentersList _presenters = new();

        public GameUiScenePresenter(GameModel gameModel, GameUiSceneView view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        public void Init()
        {
            _gameModel.ItemPlaceholderModel = new ItemPlaceholderModel();
            _gameModel.DialogsCollection = new DialogsCollection();
            _gameModel.SkillPanelModel = new SkillPanelModel(_gameModel.Specifications.SkillDeckSpecifications["first_skill_deck"], (PlayerModel)_gameModel.PlayerModel);

            _presenters.Add(new SkillPanelPresenter(_gameModel, _gameModel.SkillPanelModel, _view.SkillPanelView));
            _presenters.Add(new DeBuffPanelPresenter(_gameModel, (DeBuffsCollection)_gameModel.DeBuffsCollection, _view.DeBuffPanelView));
            _presenters.Add(new DialogsCollectionPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection, _view.DialogsCollectionView));
            _presenters.Add(new EscapeDialogPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection));
            _presenters.Add(new ItemPlaceholderPresenter(_gameModel, (ItemPlaceholderModel) _gameModel.ItemPlaceholderModel, _view.ItemPlaceholderView));
            _presenters.Add(new PlayerHealthPresenter(_gameModel, _gameModel.PlayerModel, _view.HealthResourceView));
            _presenters.Add(new PlayerAmnesiaPresenter(_gameModel, _gameModel.PlayerModel, _view.AmnesiaResourceView));
            
            _presenters.Init();
        }

        public void Dispose()
        {
            _presenters.Dispose();
        }
    }
}