using System;
using System.Linq;
using Cameras;
using DeBuff.Collection;
using Entities.Characters.Collection;
using Entities.Enemy.Collection;
using Entities.Player;
using Entities.Player.Dialog;
using Input;
using Inventory.Collection;
using Loader.Object;
using Presenter;
using Quest.Collection;
using Save.Single.Collection;
using SceneManagement.Collection;
using ServerCore.Main;
using ServerCore.Main.Commands;
using Skills.SkillPanel;
using Specifications;
using UnityEngine;
using Updater;
using Utilities.Loader.Addressable;

namespace ServerManagement.Test
{
    public class TestStartup : MonoBehaviour
    {
        public NetworkUpdaters NetworkUpdaters;
        public Transform PlayerRoot;
        public InputView InputView;
        public CameraView CameraView;
        public CharactersCollectionView CharactersCollectionView;
        
        private GameModel _gameModel;
        private readonly PresentersList _presenters = new();
        
        private async void Start()
        {
            Application.runInBackground = true;
            
            var serverConnectionModel = new ServerConnectionModel();
            
            var loadObjectsModel = new LoadObjectsModel(new AddressableObjectLoadWrapper());
            var specifications = new GameSpecifications(loadObjectsModel);
            await specifications.LoadAwaiter;

            var playerModel = new PlayerModel(specifications.EntitySpecifications[PlayerModel.ConstId]);
            
            _gameModel = new GameModel
            {
                UpdatersList = new UpdatersList(),
                FixedUpdatersList = new UpdatersList(),
                LateUpdatersList = new UpdatersList(),
                LoadObjectsModel = loadObjectsModel,
                Specifications = specifications,
                SaveSingleModelCollection = new SaveSingleModelCollection(),
                SceneManagementModelsCollection = new SceneManagementModelsCollection(),
                InputModel = new InputModel(),
                CameraModel = new CameraModel(),
                InventoriesCollection = new InventoriesCollection(specifications.InventorySpecifications.GetSpecifications(), specifications.ItemSpecifications.GetSpecifications()),
                PlayerModel = playerModel,
                EnemiesCollection = new EnemiesCollection(),
                DeBuffsCollection = new DeBuffsCollection(specifications.DeBuffSpecifications.GetSpecifications()),
                PlayerDialogModel = new PlayerDialogModel(),
                QuestsCollection = new QuestsCollection(specifications.QuestSpecifications.GetSpecifications()),
                ServerConnectionModel = serverConnectionModel,
                SkillPanelModel = new SkillPanelModel(specifications.SkillDeckSpecifications.GetSpecifications().First().Value, playerModel),
                CharactersCollection = new CharactersCollection()
            };

            Library.Initialize();
            
            var serverConnectionPresenter = new ServerConnectionPresenter(_gameModel, serverConnectionModel, NetworkUpdaters);
            serverConnectionPresenter.Init();
            
            serverConnectionModel.ConnectPlayer();
            await serverConnectionModel.CompletePlayerConnectAwaiter;
            
            serverConnectionModel.ConnectWorld();
            await serverConnectionModel.CompleteWorldConnectAwaiter;
            
            _gameModel.PlayerModel.Id = Guid.NewGuid().ToString();
            
            var command = new LoginCommand(_gameModel.PlayerModel.Id);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("finish");

            _presenters.Add(new CameraPresenter(_gameModel, (CameraModel)_gameModel.CameraModel, CameraView));
            _presenters.Add(new InputPresenter(_gameModel, (InputModel) _gameModel.InputModel, InputView));
            _presenters.Add(new PlayerPresenter(_gameModel, (PlayerModel) _gameModel.PlayerModel, PlayerRoot));
            _presenters.Add(new CharactersCollectionPresenter(_gameModel, _gameModel.CharactersCollection, CharactersCollectionView));
            _presenters.Init();
        }

        private void Update()
        {
            _gameModel?.UpdatersList.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _gameModel?.FixedUpdatersList.Update(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _gameModel?.LateUpdatersList.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Library.Deinitialize();
        }
    }
}