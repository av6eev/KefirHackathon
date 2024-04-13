﻿using Cameras;
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
using Quest.Collection;
using Save.Single.Collection;
using SceneManagement.Collection;
using ServerManagement.Test;
using Skills.SkillPanel;
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
    SkillPanelModel SkillPanelModel { get; }
    IDeBuffsCollection DeBuffsCollection { get; }
    PlayerDialogModel PlayerDialogModel { get; }
    QuestsCollection QuestsCollection { get; }
    IServerConnectionModel ServerConnectionModel { get; }
}