using Inventory;

namespace PlayerInventory.Hud
{
    public class PlayerInventoryHudModel
    {
        public readonly IInventoryModel InventoryModel;

        public PlayerInventoryHudModel(IInventoryModel hudModel)
        {
            InventoryModel = hudModel;
        }
    }
}