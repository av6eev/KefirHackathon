using Cameras;
using DeBuff.Collection;
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
using Specifications;
using UnityEngine;
using Updater;
using Utilities.Initializer;
using Utilities.Loader.Addressable;
using Utilities.Loader.Addressable.Scene;

public class Startup : MonoBehaviour
{
    [SerializeField] private InputView _inputView;
    
    private readonly PresentersList _presenters = new();
    private readonly UpdatersList _updatersList = new();
    private readonly UpdatersList _fixedUpdatersList = new();
    private readonly UpdatersList _lateUpdatersList = new();
    private GameModel _gameModel;

    private async void Start()
    {
        var loadObjectsModel = new LoadObjectsModel(new AddressableObjectLoadWrapper());
        var specifications = new GameSpecifications(loadObjectsModel);
        await specifications.LoadAwaiter;

        _gameModel = new GameModel
        {
            UpdatersList = _updatersList,
            FixedUpdatersList = _fixedUpdatersList,
            LateUpdatersList = _lateUpdatersList,
            LoadObjectsModel = loadObjectsModel,
            Specifications = specifications,
            SaveSingleModelCollection = new SaveSingleModelCollection(),
            SceneManagementModelsCollection = new SceneManagementModelsCollection(),
            InputModel = new InputModel(),
            CameraModel = new CameraModel(),
            InventoriesCollection = new InventoriesCollection(specifications.InventorySpecifications.GetSpecifications(), specifications.ItemSpecifications.GetSpecifications()),
            PlayerModel = new PlayerModel(specifications.EntitySpecifications[PlayerModel.Id]),
            EnemiesCollection = new EnemiesCollection(),
            DeBuffsCollection = new DeBuffsCollection(specifications.DeBuffSpecifications.GetSpecifications()),
            PlayerDialogModel = new PlayerDialogModel(),
            QuestsCollection = new QuestsCollection(specifications.QuestSpecifications.GetSpecifications())
        };

        _gameModel.SaveSingleModelCollection.Add(_gameModel.PlayerModel);
        _gameModel.LoadScenesModel = new LoadScenesModel(new AddressableSceneLoadWrapper(_gameModel));

        if (PlayerPrefs.GetInt("first_init") == 0)
        {
            new FirstInitializer().Initialize(_gameModel);
        }
        
        _presenters.Add(new SceneManagementModelsCollectionPresenter(_gameModel, (SceneManagementModelsCollection)_gameModel.SceneManagementModelsCollection));
        _presenters.Add(new InputPresenter(_gameModel, (InputModel) _gameModel.InputModel, _inputView));
        _presenters.Add(new InventoriesCollectionSavePresenter(_gameModel, (InventoriesCollection) _gameModel.InventoriesCollection));
        _presenters.Add(new SaveSingleModelCollectionPresenter(_gameModel, (SaveSingleModelCollection)_gameModel.SaveSingleModelCollection));
        _presenters.Init();

        _gameModel.SceneManagementModelsCollection.Load(SceneConst.GameUiId);
        _gameModel.SceneManagementModelsCollection.Load(_gameModel.PlayerModel.BaseLocationId); 
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
    }
}
