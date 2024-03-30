using Entities.Enemy.Specification;
using Entities.Specification;
using Utilities.ModelCollection;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollection : ModelCollection<EnemyModel>, IEnemiesCollection
    {
        public void AddEnemy(EnemySpecification specification, IEntityModel target, EntitySpecification entitySpecification)
        {
            Add(new EnemyModel(entitySpecification, target, specification));
        }
    }
}