using System;

namespace Quest.Demands.Specification.KillEnemy
{
    [Serializable]
    public class KillEnemyDemandSpecification : BaseDemandSpecification
    {
        public int KillCount;
        
        public override bool Verify(IGameModel gameModel)
        {
            return gameModel.PlayerModel.KillCount.Value >= KillCount;
        }
    }
}