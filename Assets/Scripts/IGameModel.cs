using Cameras;
using Dialogs.Collection;
using Entities.Enemy.Collection;
using Entities.Player;
using Input;
using Inventory.Collection;
using Item.ItemPlaceholder;
using Loader.Object;
using Loader.Scene;
using Save.Single.Collection;
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
    ISaveSingleModelCollection SaveSingleModelCollection { get; }
    ISceneManagementModelsCollection SceneManagementModelsCollection { get; }
    IInputModel InputModel { get; }
    IPlayerModel PlayerModel { get; }
    ICameraModel CameraModel { get; }
    IInventoriesCollection InventoriesCollection { get; }
    IDialogsCollection DialogsCollection { get; }
    IItemPlaceholderModel ItemPlaceholderModel { get; }
    IEnemiesCollection EnemiesCollection { get; }
}