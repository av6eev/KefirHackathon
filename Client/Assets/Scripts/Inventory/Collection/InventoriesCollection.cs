using System;
using System.Collections.Generic;
using Inventory.Slot;
using Inventory.Specification;
using Item.Specification;
using SimpleJson;
using Utilities;
using Utilities.ModelCollection;

namespace Inventory.Collection
{
    public class InventoriesCollection : ModelCollection<string, InventoryModel>, IInventoriesCollection
    {
        public const string Id = "inventories";
        public Dictionary<string, InventorySpecification> Specifications { get; }
        private readonly Dictionary<string, ItemSpecification> _itemSpecifications;

        public InventoriesCollection(Dictionary<string, InventorySpecification> specifications, Dictionary<string, ItemSpecification> itemSpecifications)
        {
            _itemSpecifications = itemSpecifications;
            Specifications = specifications;
        }
        
        public void FillFromSave(List<IDictionary<string, object>> nodes)
        {
            foreach (var node in nodes)
            {       
                var inventoryId = node.GetString("id");
                var specificationId = node.GetString("specification_id");
                var slots = node.GetNodes("slots");

                CreateInventory(specificationId, inventoryId, slots);
            }
        }
        
        public string CreateInventory(string specificationId)
        {
            var newInventory = new InventoryModel(Specifications[specificationId], _itemSpecifications, ChangeEvent);
            Add(newInventory.Id, newInventory);

            return newInventory.Id;
        }
        
        public string CreateInventory(string specificationId, string id, List<IDictionary<string, object>> savedSlots = null)
        {
            var slots = new Dictionary<int, InventorySlotModel>();
            InventoryModel newInventory;
            
            if (savedSlots != null)
            {
                foreach (var slot in savedSlots)
                {
                    var index = slot.GetString("index");
                    var itemId = slot.GetString("item_id");
                    var itemAmount = slot.GetString("item_amount");

                    var indexToInt = Convert.ToInt32(index);
                    slots.Add(indexToInt, new InventorySlotModel(indexToInt, itemId, id, Convert.ToInt32(itemAmount)));
                }    
                
                newInventory = new InventoryModel(Specifications[specificationId], _itemSpecifications, id, ChangeEvent, slots);
            }
            else
            {
                newInventory = new InventoryModel(Specifications[specificationId], _itemSpecifications, id, ChangeEvent);
            }
                        
            Add(newInventory.Id, newInventory);

            return newInventory.Id;
        }
    }
}