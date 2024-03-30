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

public class GameModel : IGameModel
{
    public IUpdatersList UpdatersList { get; set; }
    public IUpdatersList FixedUpdatersList { get; set; }
    public IUpdatersList LateUpdatersList { get; set; }
    public IGameSpecifications Specifications { get; set; }
    public ILoadScenesModel LoadScenesModel { get; set; }
    public ILoadObjectsModel LoadObjectsModel { get; set; }
    public ISaveSingleModelCollection SaveSingleModelCollection { get; set; }
    public ISceneManagementModelsCollection SceneManagementModelsCollection { get; set; }
    public IInputModel InputModel { get; set; }
    public IPlayerModel PlayerModel { get; set; }
    public ICameraModel CameraModel { get; set; }
    public IInventoriesCollection InventoriesCollection { get; set; }
    public IDialogsCollection DialogsCollection { get; set; }
    public IItemPlaceholderModel ItemPlaceholderModel { get; set; }
    public IEnemiesCollection EnemiesCollection { get; set; }
}