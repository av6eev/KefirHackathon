using Dialogs;
using Dialogs.Specification;

namespace Inventory.Dialog
{
    public class InventoryDialogModel : DialogModel
    {
        public readonly IInventoryModel InventoryModel;

        public InventoryDialogModel(IInventoryModel inventoryModel, DialogSpecification specification) : base(specification)
        {
            InventoryModel = inventoryModel;
        }
    }
}