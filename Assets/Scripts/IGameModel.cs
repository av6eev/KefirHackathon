using Loader.Object;
using Loader.Scene;
using SceneManagement.Collection;
using Specifications;
using Updater;

public interface IGameModel : IBaseGameModel
{
    IUpdatersList UpdatersList { get; }
    IUpdatersList FixedUpdatersList { get; }
    IUpdatersList LateUpdatersList { get; }
    IGameSpecifications Specifications { get; }
    ILoadScenesModel LoadScenesModel { get; }
    ILoadObjectsModel LoadObjectsModel { get; }
    ISceneManagementModelsCollection SceneManagementModelsCollection { get; }
}