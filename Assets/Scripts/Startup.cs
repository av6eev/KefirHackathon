using Loader.Object;
using Loader.Scene;
using Presenter;
using Save.Single.Collection;
using SceneManagement;
using SceneManagement.Collection;
using Specifications;
using UnityEngine;
using Updater;
using Utilities.Loader.Addressable;
using Utilities.Loader.Addressable.Scene;

public class Startup : MonoBehaviour
{
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
        };

        _gameModel.LoadScenesModel = new LoadScenesModel(new AddressableSceneLoadWrapper(_gameModel));

        _presenters.Add(new SceneManagementModelsCollectionPresenter(_gameModel, (SceneManagementModelsCollection)_gameModel.SceneManagementModelsCollection));
        _presenters.Add(new SaveSingleModelCollectionPresenter(_gameModel, (SaveSingleModelCollection) _gameModel.SaveSingleModelCollection));
        _presenters.Init();

        _gameModel.SceneManagementModelsCollection.Load(SceneConst.GameUiId);
        _gameModel.SceneManagementModelsCollection.Load(SceneConst.HubId);
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
