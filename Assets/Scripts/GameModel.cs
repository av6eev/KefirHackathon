using Loader.Object;
using Loader.Scene;
using SceneManagement.Collection;
using Specifications;
using Updater;

public class GameModel : IGameModel
{
    public IUpdatersList UpdatersList { get; set; }
    public IUpdatersList FixedUpdatersList { get; set; }
    public IUpdatersList LateUpdatersList { get; set; }
    public IGameSpecifications Specifications { get; set; }
    public ILoadScenesModel LoadScenesModel { get; set; }
    public ILoadObjectsModel LoadObjectsModel { get; set; }
    public ISceneManagementModelsCollection SceneManagementModelsCollection { get; set; }
}