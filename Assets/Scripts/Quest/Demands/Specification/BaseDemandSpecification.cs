using Specification;

namespace Quest.Demands.Specification
{
    public class BaseDemandSpecification : BaseSpecification
    {
        public virtual bool Verify(IGameModel gameModel)
        {
            return true;
        }
    }
}