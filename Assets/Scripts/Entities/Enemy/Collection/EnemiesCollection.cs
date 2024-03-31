using Entities.Enemy.Specification;
using Utilities.ModelCollection;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollection : ModelCollection<EnemyModel>, IEnemiesCollection
    {
        public const int MaxEnemiesCount = 12;
        public void AddEnemy(EnemySpecification specification, IEntityModel target)
        {
            Add(new EnemyModel(specification, target));
        }
    }
}