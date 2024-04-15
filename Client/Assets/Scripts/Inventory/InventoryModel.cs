using System;
using System.Collections.Generic;
using Inventory.Slot;
using Inventory.Specification;
using Item.Specification;
using Reactive.Event;
using Utilities;
using Utilities.ModelCollection;

namespace Inventory
{
    public class InventoryModel : ModelCollection<InventorySlotModel>, IInventoryModel
    {
        private readonly Dictionary<string, ItemSpecification> _itemSpecifications;
        public string Id { get; }
        public bool IsActive { get; private set; }
        public InventorySpecification Specification { get; }
        public ReactiveEvent SaveEvent { get; }

        public InventoryModel(InventorySpecification specification, Dictionary<string, ItemSpecification> itemSpecifications, string id, ReactiveEvent saveEvent, Dictionary<int, InventorySlotModel> savedSlots = null)
        {
            Id = id;
            Specification = specification;
            _itemSpecifications = itemSpecifications;
            SaveEvent = saveEvent;
            
            FirstInit(savedSlots);
        }

        public InventoryModel(InventorySpecification specification, Dictionary<string, ItemSpecification> itemSpecifications, ReactiveEvent saveEvent)
        {
            Id = Guid.NewGuid().ToString();
            Specification = specification;
            _itemSpecifications = itemSpecifications;
            SaveEvent = saveEvent;
            
            FirstInit();
        }

        private void FirstInit(Dictionary<int, InventorySlotModel> savedSlots = null)
        {
            var size = Specification.SlotCount;
            
            for (var i = 0; i < size; i++)
            {
                if (savedSlots == null || savedSlots.Count == 0)
                {
                    Add(new InventorySlotModel(i, Id));
                }
                else
                {
                    if (savedSlots.TryGetValue(i, out var slot))
                    {
                        Add(slot);
                    }
                    else
                    {
                        Add(new InventorySlotModel(i, Id));
                    }
                }
            }
        }

        public IInventorySlotModel GetActiveSlot()
        {
            return Collection.Find(slot => slot.IsActive.Value);
        }

        public IDictionary<string, object> GetSaveData()
        {
            var slots = new List<Dictionary<string, object>>();

            foreach (var slot in Collection)
            {
                if (slot.IsEmpty)
                {
                    continue;
                }

                var element = new Dictionary<string, object>()
                {
                    { "index", slot.Index },
                    { "item_id", slot.ItemId },
                    { "item_amount", slot.ItemAmount },
                };

                slots.Add(element);
            }
            
            return new Dictionary<string, object>
            {
                { "id", Id },
                { "specification_id", Specification.Id },
                { "slots", slots }
            };
        }

        public InventorySlotModel GetSlot(int index)
        {
            return Collection[index];
        }
        
        public void AddItem(string itemId, int amount = 1)
        {
            var remaining = amount;
            AddToSlotWithSameItem(itemId, _itemSpecifications[itemId], remaining, out remaining);

            if (remaining <= 0)
            {
                return;
            }

            AddToFirstAvailableSlot(itemId, _itemSpecifications[itemId], remaining, out remaining);
        }

        public void AddItem(int index, string itemId, int amount = 1)
        {
            var slot = Collection[index];
            var newValue = slot.ItemAmount + amount;

            if (slot.IsEmpty)
            {
                slot.ItemId = itemId;
            }

            var itemCapacity = _itemSpecifications[itemId].MaxSlotCapacity;

            if (newValue > itemCapacity)
            {
                var remaining = newValue - itemCapacity;
                slot.ItemAmount = itemCapacity;

                AddItem(itemId, remaining);
            }
            else
            {
                slot.ItemAmount = newValue;
                SaveEvent.Invoke();
            }
        }

        public void RemoveItem(string itemId, int amount = 1)
        {
            if (!IsContainsItem(itemId, amount))
            {
                return;
            }

            var toRemoveAmount = amount;

            foreach (var slot in Collection)
            {
                if (slot.ItemId != itemId)
                {
                    continue;
                }

                if (toRemoveAmount > slot.ItemAmount)
                {
                    toRemoveAmount -= slot.ItemAmount;

                    RemoveItem(slot.Index, slot.ItemAmount);
                    
                    if (toRemoveAmount == 0)
                    {
                        return;
                    }
                }
                else
                {
                    RemoveItem(slot.Index, toRemoveAmount);
                    return;
                }
            }

            throw new Exception($"Couldn't remove item: {itemId} with amount: {amount}");
        }

        public void RemoveItem(int position, int amount = 1)
        {
            var slot = Collection[position];

            if (slot.IsEmpty || slot.ItemAmount < amount)
            {
                return;
            }

            slot.ItemAmount -= amount;

            if (slot.ItemAmount != 0)
            {
                return;
            }
            
            slot.ItemId = null;
            
            SaveEvent.Invoke();
        }

        private void AddToFirstAvailableSlot(string itemId, ItemSpecification specification, int amount, out int remaining)
        {
            remaining = amount;

            foreach (var slot in Collection)
            {
                if (!slot.IsEmpty)
                {
                    continue;
                }

                slot.ItemId = itemId;

                var newValue = remaining;
                var itemCapacity = specification.MaxSlotCapacity;

                if (newValue > itemCapacity)
                {
                    remaining = newValue - itemCapacity;
                    slot.ItemAmount = itemCapacity;
                    SaveEvent.Invoke();
                }
                else
                {
                    slot.ItemAmount = newValue;
                    remaining = 0;
                    
                    SaveEvent.Invoke();
                    return;
                }
            }
        }

        private void AddToSlotWithSameItem(string itemId, ItemSpecification specification, int amount, out int remaining)
        {
            remaining = amount;

            foreach (var slot in Collection)
            {
                if (slot.IsEmpty)
                {
                    continue;
                }

                var itemCapacity = specification.MaxSlotCapacity;

                if (slot.ItemAmount >= itemCapacity)
                {
                    continue;
                }

                if (slot.ItemId != itemId)
                {
                    continue;
                }

                var newValue = slot.ItemAmount + remaining;

                if (newValue > itemCapacity)
                {
                    remaining = newValue - itemCapacity;

                    slot.ItemAmount = itemCapacity;
                    
                    SaveEvent.Invoke();
                    
                    if (remaining == 0)
                    {
                        return;
                    }
                }
                else
                {
                    slot.ItemAmount = newValue;
                    remaining = 0;
                    
                    SaveEvent.Invoke();
                    return;
                }
            }
        }

        public void SwitchSlots(int firstSlotIndex, int secondSlotIndex)
        {
            var firstSlot = Collection[firstSlotIndex];
            var secondSlot = Collection[secondSlotIndex];
            var firstSlotItemId = firstSlot.ItemId;
            var firstSlotItemAmount = firstSlot.ItemAmount;

            firstSlot.ItemId = secondSlot.ItemId;
            firstSlot.ItemAmount = secondSlot.ItemAmount;
            secondSlot.ItemId = firstSlotItemId;
            secondSlot.ItemAmount = firstSlotItemAmount;
            
            SaveEvent.Invoke();
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            
            foreach (var slot in Collection)
            {
                if (slot.ItemId == itemId)
                {
                    amount += slot.ItemAmount;
                }    
            }

            return amount;
        }

        public bool IsContainsItem(string itemId, int amount)
        {
            return GetAmount(itemId) >= amount;
        }

        public void SetActive(bool state)
        {
            IsActive = state;
        }
    }
}