using System.Collections.Generic;
using Dialogs;
using Inventory.Slot;
using UnityEngine;

namespace Inventory.Dialog
{
    public class InventoryDialogView : DialogView
    {
        public Transform ContentRoot;
        public List<InventorySlotView> Slots;
    }
}