using Awaiter;
using Cameras.Specification;
using DeBuff.Specification;
using Dialogs.Specification;
using Entities.Enemy.Specification;
using Entities.Specification;
using Inventory.Specification;
using Item.Specification;
using Loader.Object;
using Quest.Demands.Specification;
using Quest.Rewards.Specification;
using Quest.Specification;
using Skills.Deck;
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
        public ISpecificationsCollection<EnemySpecification> EnemySpecifications { get; } = new SpecificationsCollection<EnemySpecification>();
        public ISpecificationsCollection<SkillDeckSpecification> SkillDeckSpecifications { get; } = new SpecificationsCollection<SkillDeckSpecification>();
        public ISpecificationsCollection<DeBuffSpecification> DeBuffSpecifications { get; } = new SpecificationsCollection<DeBuffSpecification>();
        public ISpecificationsCollection<BaseDemandSpecification> DemandSpecifications { get; } = new SpecificationsCollection<BaseDemandSpecification>();
        public ISpecificationsCollection<BaseRewardSpecification> RewardSpecifications { get; } = new SpecificationsCollection<BaseRewardSpecification>();
        public ISpecificationsCollection<QuestSpecification> QuestSpecifications { get; } = new SpecificationsCollection<QuestSpecification>();
        public ISpecificationsCollection<DialogSpecification> DialogSpecifications { get; } = new SpecificationsCollection<DialogSpecification>();

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
            await new LoadSpecificationsWrapper<EnemySpecification>(loadObjectsModel, "enemies", EnemySpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<SkillDeckSpecification>(loadObjectsModel, "skill_decks", SkillDeckSpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<DeBuffSpecification>(loadObjectsModel, "de_buffs", DeBuffSpecifications).LoadAwaiter;
            await new LoadSpecificationsWrapper<QuestSpecification>(loadObjectsModel, "quests", QuestSpecifications).LoadAwaiter;
            await new LoadAssetsSpecificationsWrapper<DialogSpecification>(loadObjectsModel, "dialogs", DialogSpecifications).LoadAwaiter;
            await new LoadAssetsSpecificationsWrapper<BaseDemandSpecification>(loadObjectsModel, "demands", DemandSpecifications).LoadAwaiter;
            await new LoadAssetsSpecificationsWrapper<BaseRewardSpecification>(loadObjectsModel, "rewards", RewardSpecifications).LoadAwaiter;

            LoadAwaiter.Complete();
        }
    }
}