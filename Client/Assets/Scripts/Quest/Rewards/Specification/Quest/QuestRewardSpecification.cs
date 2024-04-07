using System;

namespace Quest.Rewards.Specification.Quest
{
    [Serializable]
    public class QuestRewardSpecification : BaseRewardSpecification
    {
        public string QuestId;
        
        public override void Give(IGameModel gameModel)
        {
            gameModel.QuestsCollection.AddQuest(QuestId);
        }
    }
}