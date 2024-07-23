using DeBuff.Collection;
using Dialogs;
using Dialogs.Collection;
using Entities.Player;
using GameScenes.GameUI.DeBuffPanel;
using GameScenes.GameUI.DebugPanel;
using GameScenes.GameUI.EnterNicknamePanel;
using GameScenes.GameUI.FriendsPanel;
using GameScenes.GameUI.PartyPanel;
using GameScenes.GameUI.QuestPanel;
using Item.ItemPlaceholder;
using LoadingScreen;
using Presenter;
using Skills.SkillPanel;
using UnityEngine;
using Utilities.Initializer;

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
            _gameModel.InputModel.Disable();
            
            _gameModel.ItemPlaceholderModel = new ItemPlaceholderModel();
            _gameModel.DialogsCollection = new DialogsCollection();
            _gameModel.SkillPanelModel = new SkillPanelModel(_gameModel.Specifications.SkillDeckSpecifications["first_skill_deck"], (PlayerModel)_gameModel.PlayerModel);
            _gameModel.DebugPanelModel = new DebugPanelModel();
            
            _presenters.Add(new EnterNicknamePanelPresenter(_gameModel, _gameModel.EnterNicknamePanelModel, _view.EnterNicknamePanelView));
            _presenters.Add(new DialogsCollectionPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection, _view.DialogsCollectionView));
            _presenters.Add(new SkillPanelPresenter(_gameModel, _gameModel.SkillPanelModel, _view.SkillPanelView));
            _presenters.Add(new DeBuffPanelPresenter(_gameModel, (DeBuffsCollection)_gameModel.DeBuffsCollection, _view.DeBuffPanelView));
            _presenters.Add(new EscapeDialogPresenter(_gameModel, (DialogsCollection)_gameModel.DialogsCollection));
            _presenters.Add(new ItemPlaceholderPresenter(_gameModel, (ItemPlaceholderModel) _gameModel.ItemPlaceholderModel, _view.ItemPlaceholderView));
            _presenters.Add(new PlayerHealthPresenter(_gameModel, _gameModel.PlayerModel, _view.HealthResourceView));
            _presenters.Add(new PlayerAmnesiaPresenter(_gameModel, _gameModel.PlayerModel, _view.AmnesiaResourceView));
            _presenters.Add(new QuestPanelPresenter(_gameModel, _gameModel.QuestsCollection, _view.QuestPanelView));
            _presenters.Add(new DebugPanelPresenter(_gameModel, _gameModel.DebugPanelModel, _view.DebugPanelView));
            _presenters.Add(new PartyPanelPresenter(_gameModel, new PartyPanelModel(), _view.PartyPanelView));
            _presenters.Add(new FriendsPanelPresenter(_gameModel, new FriendsPanelModel(), _view.FriendsPanelView));
            _presenters.Add(new LoadingScreenPresenter(_gameModel, (LoadingScreenModel)_gameModel.LoadingScreenModel, _view.LoadingScreenView));
            _presenters.Init();
        }

        public void Dispose()
        {
            _presenters.Dispose();
        }
    }
}