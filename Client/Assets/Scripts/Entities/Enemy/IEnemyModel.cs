using Entities.Enemy.Specification;
using Entities.Enemy.State;
using Reactive.Field;
using Utilities;
using Utilities.Model;

namespace Entities.Enemy
{
    public interface IEnemyModel : IEntityModel, IModel
    {
        public EnemySpecification EnemySpecification { get; }
        ReactiveField<EnemyStateType> CurrentState { get; }
        bool IsAgro { get; }
    }
}