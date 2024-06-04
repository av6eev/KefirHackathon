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
using Loader.Scene;
using Presenter;
using Quest.Collection;
using Save.Single.Collection;
using SceneManagement;
using SceneManagement.Collection;
using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Specifications;
using ServerCore.Main.Users;
using ServerCore.Main.Utilities.LoadWrapper.Json;
using ServerCore.Main.World;
using ServerManagement.Test;
using Skills.SkillPanel;
using Specifications;
using UnityEngine;
using Updater;
using Utilities.Initializer;
using Utilities.Loader.Addressable;
using Utilities.Loader.Addressable.Scene;

public class Startup : MonoBehaviour
{
    public InputView InputView;
    public NetworkUpdaters NetworkUpdaters;

    private readonly PresentersList _presenters = new();
    private readonly UpdatersList _updatersList = new();
    private readonly UpdatersList _fixedUpdatersList = new();
    private readonly UpdatersList _lateUpdatersList = new();
    
    private GameModel _gameModel;

    private async void Start()
    {
        Application.runInBackground = true;
            
        var serverConnectionModel = new ServerConnectionModel();
            
        var loadObjectsModel = new LoadObjectsModel(new AddressableObjectLoadWrapper());
        var specifications = new GameSpecifications(loadObjectsModel);
        await specifications.LoadAwaiter;

        var serverLoadObjectsModel = new ServerCore.Main.Utilities.LoadWrapper.Object.LoadObjectsModel(new JsonObjectLoadWrapper(), "../ServerCore/Main/Specifications");
        var serverSpecifications = new ServerSpecifications(serverLoadObjectsModel);
        await serverSpecifications.LoadAwaiter;

        Debug.Log(serverSpecifications.LocationSpecifications.GetSpecifications().Count);
        Debug.Log(serverSpecifications.InteractObjectStateSpecifications.GetSpecifications().Count);
            
        var playerModel = new PlayerModel(specifications.EntitySpecifications[PlayerModel.ConstId]);

        _gameModel = new GameModel
        {
            UpdatersList = _updatersList,
            FixedUpdatersList = _fixedUpdatersList,
            LateUpdatersList = _lateUpdatersList,
            LoadObjectsModel = loadObjectsModel,
            Specifications = specifications,
            ServerSpecifications = serverSpecifications,
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
            SkillPanelModel = new SkillPanelModel(specifications.SkillDeckSpecifications.GetSpecifications().First().Value, playerModel),
            CharactersCollection = new CharactersCollection(),
            UserData = new UserData(),
            WorldData = new WorldData(string.Empty),
            ServerConnectionModel = serverConnectionModel
        };
        
        Library.Initialize();
            
        var serverConnectionPresenter = new ServerConnectionPresenter(_gameModel, serverConnectionModel, NetworkUpdaters);
        serverConnectionPresenter.Init();
            
        serverConnectionModel.ConnectPlayer();
        await serverConnectionModel.CompletePlayerConnectAwaiter;
            
        _gameModel.PlayerModel.Id = Guid.NewGuid().ToString();
            
        var command = new LoginCommand(_gameModel.PlayerModel.Id);
        command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
        Debug.Log("finish");
        
        _gameModel.SaveSingleModelCollection.Add(_gameModel.PlayerModel);
        _gameModel.LoadScenesModel = new LoadScenesModel(new AddressableSceneLoadWrapper(_gameModel));

        if (PlayerPrefs.GetInt("first_init") == 0)
        {
            new FirstInitializer().Initialize(_gameModel);
        }
        
        _presenters.Add(new SceneManagementModelsCollectionPresenter(_gameModel, (SceneManagementModelsCollection)_gameModel.SceneManagementModelsCollection));
        _presenters.Add(new InputPresenter(_gameModel, (InputModel) _gameModel.InputModel, InputView));
        _presenters.Add(new InventoriesCollectionSavePresenter(_gameModel, (InventoriesCollection) _gameModel.InventoriesCollection));
        _presenters.Add(new SaveSingleModelCollectionPresenter(_gameModel, (SaveSingleModelCollection)_gameModel.SaveSingleModelCollection));
        _presenters.Init();

        _gameModel.SceneManagementModelsCollection.Load(SceneConst.GameUiId);

        var userLastLocationId = _gameModel.UserData.CurrentLocationId.Value;
        
        if (userLastLocationId == string.Empty || userLastLocationId == _gameModel.PlayerModel.BaseLocationId)
        {
            _gameModel.SceneManagementModelsCollection.Load(_gameModel.PlayerModel.BaseLocationId); 
        }
        else
        {
            _gameModel.SceneManagementModelsCollection.Load(userLastLocationId);
        }
    }

    private void Update()
    {
        _updatersList.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _fixedUpdatersList.Update(Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        _lateUpdatersList.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _presenters.Dispose();
        _presenters.Clear();

        _updatersList.Clear();
        _fixedUpdatersList.Clear();
        _lateUpdatersList.Clear();
    }
}
