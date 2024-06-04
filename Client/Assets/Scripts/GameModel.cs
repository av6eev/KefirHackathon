using Cameras;
using DeBuff.Collection;
using Dialogs.Collection;
using Entities.Characters.Collection;
using Entities.Enemy.Collection;
using Entities.Player;
using Entities.Player.Dialog;
using Input;
using Inventory.Collection;
using Item.ItemPlaceholder;
using Loader.Object;
using Loader.Scene;
using Quest.Collection;
using Save.Single.Collection;
using SceneManagement.Collection;
using ServerCore.Main.Specifications;
using ServerCore.Main.Users;
using ServerCore.Main.World;
using ServerManagement.Test;
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
    public QuestsCollection QuestsCollection { get; set; }
    public IServerConnectionModel ServerConnectionModel { get; set; }
    public CharactersCollection CharactersCollection { get; set; }
    public bool Rerun { get; set; }
    public WorldData WorldData { get; set; }
    public UserData UserData { get; set; }
    public IServerSpecifications ServerSpecifications { get; set; }
}