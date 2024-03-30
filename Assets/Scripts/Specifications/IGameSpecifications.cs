using Cameras.Specification;
using Entities.Enemy.Specification;
using Entities.Specification;
using Inventory.Specification;
using Item.Specification;
using Specification.Scene;
using Specifications.Collection;

namespace Specifications
{
    public interface IGameSpecifications
    {
        ISpecificationsCollection<SceneSpecification> SceneSpecifications { get; }
        ISpecificationsCollection<CameraSpecification> CameraSpecifications { get; }
        ISpecificationsCollection<EntitySpecification> EntitySpecifications { get; }
        ISpecificationsCollection<ItemSpecification> ItemSpecifications { get; }
        ISpecificationsCollection<InventorySpecification> InventorySpecifications { get; }
        ISpecificationsCollection<EnemySpecification> EnemySpecifications { get; }
    }
}