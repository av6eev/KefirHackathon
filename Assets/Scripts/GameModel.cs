using Cameras;
using DeBuff.Collection;
using Dialogs.Collection;
using Entities.Enemy.Collection;
using Entities.Player;
using Entities.Player.Dialog;
using Input;
using Inventory.Collection;
using Item.ItemPlaceholder;
using Loader.Object;
using Loader.Scene;
using Save.Single.Collection;
using SceneManagement.Collection;
using Skills.SkillPanel;
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
    public SkillPanelModel SkillPanelModel { get; set; }
    public IDeBuffsCollection DeBuffsCollection { get; set; }
    public PlayerDialogModel PlayerDialogModel { get; set; }
}