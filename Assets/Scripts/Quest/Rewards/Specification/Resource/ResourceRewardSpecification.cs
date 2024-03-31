using System;
using Entities;

namespace Quest.Rewards.Specification.Resource
{
    [Serializable]
    public class ResourceRewardSpecification : BaseRewardSpecification
    {
        public EntityResourceType ResourceType;
        public int Amount;
        
        public override void Give(IGameModel gameModel)
        {
            gameModel.PlayerModel.Resources.GetModel(ResourceType).Increase(Amount);
        }
    }
}