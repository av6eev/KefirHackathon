using Reactive.Field;
using Utilities.Model;

namespace Inventory.Slot
{
    public interface IInventorySlotModel : IModel
    {
        int Index { get; }
        string ItemId { get; }
        int ItemAmount { get; }
        bool IsEmpty { get; }
        ReactiveField<bool> IsActive { get; }
    }
}