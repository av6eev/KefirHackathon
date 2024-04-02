using System;
using Item;
using Reactive.Field;

namespace Inventory.Slot
{
    public class InventorySlotModel : IInventorySlotModel
    {
        public event Action<string> OnItemIdChanged;
        public event Action<int> OnItemAmountChanged;
        
        public int Index { get; }
        
        private string _itemId;
        public string ItemId
        {
            get => _itemId;
            set
            {
                if (_itemId == value)
                {
                    return;
                }

                _itemId = value;
                OnItemIdChanged?.Invoke(value);
            }
        }

        private int _itemAmount;
        public int ItemAmount
        {
            get => _itemAmount;
            set
            {
                if (_itemAmount == value)
                {
                    return;
                }
                
                _itemAmount = value;
                OnItemAmountChanged?.Invoke(value);
            }
        }

        public string InventoryId { get; }
        public bool IsEmpty => ItemAmount == 0 && string.IsNullOrEmpty(ItemId);
        public ReactiveField<bool> IsActive { get; } = new(false);
        public bool IsNeedType { get; set; }
        public ItemType ItemType { get; set; }

        public InventorySlotModel(int index, string itemId, string inventoryId, int itemAmount)
        {
            Index = index;
            ItemId = itemId;
            InventoryId = inventoryId;
            ItemAmount = itemAmount;
        }

        public InventorySlotModel(int index, string inventoryId)
        {
            Index = index;
            InventoryId = inventoryId;
        }
    }
}