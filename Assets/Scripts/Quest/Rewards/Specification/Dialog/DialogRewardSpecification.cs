using System;

namespace Quest.Rewards.Specification.Dialog
{
    [Serializable]
    public class DialogRewardSpecification : BaseRewardSpecification
    {
        public string[] Texts;
        
        public override void Give(IGameModel gameModel)
        {
            foreach (var text in Texts)
            {
                gameModel.PlayerDialogModel.Add(text);
            }
        }
    }
}