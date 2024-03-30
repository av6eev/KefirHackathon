using Entities.Enemy.Specification;
using Entities.Enemy.State;
using Entities.Specification;
using Reactive.Field;

namespace Entities.Enemy
{
    public class EnemyModel : EntityModel, IEnemyModel
    {
        public EnemySpecification EnemySpecification { get; }

        public ReactiveField<EnemyStateType> CurrentState { get; } = new(EnemyStateType.Idle);
        
        public bool IsAgro { get; set; }

        public EnemyModel(EntitySpecification entitySpecification, IEntityModel target, EnemySpecification enemySpecification) : base(entitySpecification, target)
        {
            EnemySpecification = enemySpecification;
        }
    }
}