using Awaiter;
using Cameras.Specification;
using Entities.Specification;
using Inventory.Specification;
using Item.Specification;
using Loader.Object;
using Specification.Scene;
using Specifications.Collection;
using Specifications.LoadWrapper;

namespace Specifications
{
    public class GameSpecifications : IGameSpecifications
    {
        public ISpecificationsCollection<SceneSpecification> SceneSpecifications { get; } = new SpecificationsCollection<SceneSpecification>();
        public ISpecificationsCollection<CameraSpecification> CameraSpecifications { get; } = new SpecificationsCollection<CameraSpecification>();
        public ISpecificationsCollection<EntitySpecification> EntitySpecifications { get; } = new SpecificationsCollection<EntitySpecification>();
        public ISpecificationsCollection<ItemSpecification> ItemSpecifications { get; } = new SpecificationsCollection<ItemSpecification>();
        public ISpecificationsCollection<InventorySpecification> InventorySpecifications { get; } = new SpecificationsCollection<InventorySpecification>();

        public readonly CustomAwaiter LoadAwaiter = new();
        
        public GameSpecifications(ILoadObjectsModel loadObjectsModel)
        {
            Load(loadObjectsModel);
        }

        private async void Load(ILoadObjectsModel loadObjectsModel)
        {
            await new LoadSpecificationsWrapper<SceneSpecification>(loadObjectsModel, "scenes", SceneSpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<CameraSpecification>(loadObjectsModel, "cameras", CameraSpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<EntitySpecification>(loadObjectsModel, "entities", EntitySpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<ItemSpecification>(loadObjectsModel, "items", ItemSpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<InventorySpecification>(loadObjectsModel, "inventories", InventorySpecifications).LoadAwaiter;
            
            LoadAwaiter.Complete();
        }
    }
}