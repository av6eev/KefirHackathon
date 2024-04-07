using System.Collections.Generic;
using Utilities.ModelCollection;

namespace Inventory.Collection
{
    public interface IInventoriesCollection : IModelCollection<string, InventoryModel>
    {
        string CreateInventory(string specificationId);
        string CreateInventory(string specificationId, string id, List<IDictionary<string, object>> savedSlots = null);
    }
}