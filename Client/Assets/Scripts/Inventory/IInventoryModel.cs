using Inventory.Slot;
using Inventory.Specification;
using Save;
using Utilities.ModelCollection;

namespace Inventory
{
    public interface IInventoryModel : IModelCollection<InventorySlotModel>, ISaveModel
    {
        string Id { get; }
        InventorySpecification Specification { get; }
        bool IsActive { get; }
        int GetAmount(string itemId);
        IInventorySlotModel GetActiveSlot();
        bool IsContainsItem(string itemId, int amount);
        void SwitchSlots(int firstSlotIndex, int secondSlotIndex);
        void AddItem(string itemId, int amount = 1);
        void AddItem(int index, string itemId, int amount = 1);
        void RemoveItem(string itemId, int amount = 1);
        void RemoveItem(int position, int amount = 1);
        void SetActive(bool state);
    }
}