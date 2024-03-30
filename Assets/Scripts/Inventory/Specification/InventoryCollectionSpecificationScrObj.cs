using Specifications;
using UnityEngine;

namespace Inventory.Specification
{
    [CreateAssetMenu(menuName = "Create Specification Collection/New Inventory Collection", fileName = "InventoryCollectionSpecification", order = 51)]
    public class InventoryCollectionSpecificationScrObj : SpecificationCollectionScrObj<InventorySpecification>
    {
    }
}