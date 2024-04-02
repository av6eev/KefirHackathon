using Entities;
using Entities.Player;
using Item;
using UnityEngine;

namespace Utilities.Initializer
{
    public class FirstInitializer
    {
        public void Initialize(GameModel gameModel)
        {
            gameModel.InventoriesCollection.CreateInventory("player_hud_inventory", PlayerModel.HudId);
                
            var playerHudInventory = gameModel.InventoriesCollection.GetModel(PlayerModel.HudId);
                
            playerHudInventory.AddItem(ItemConst.SimpleSwordId);

            gameModel.PlayerModel.Resources.GetModel(EntityResourceType.Essence).Increase(100);
            
            gameModel.PlayerDialogModel.Add("Сколько же лет прошло?");
            gameModel.PlayerDialogModel.Add("Похоже, долго я был в отключке...");
            gameModel.PlayerDialogModel.Add("Главное, почему же я здесь?");
            gameModel.PlayerDialogModel.Add("Единственное, что помню: WASD");
            gameModel.PlayerDialogModel.Add("А, ну еще Ctrl и Shift...");
            
            PlayerPrefs.SetInt("first_init", 1);    
        }
    }
}