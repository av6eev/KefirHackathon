﻿using Entities.Enemy.Specification;
using Utilities.ModelCollection;

namespace Entities.Enemy.Collection
{
    public interface IEnemiesCollection : IModelCollection<EnemyModel>
    {
        void AddEnemy(EnemySpecification specification, IEntityModel target);
    }
}