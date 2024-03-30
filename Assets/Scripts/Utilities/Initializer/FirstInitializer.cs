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
            
            PlayerPrefs.SetInt("first_init", 1);    
        }
    }
}