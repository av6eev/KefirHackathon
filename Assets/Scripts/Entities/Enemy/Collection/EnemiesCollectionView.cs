using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollectionView : MonoBehaviour
    {
        public Transform Root;
        public Transform SpawnZoneCenter;
        public List<EnemyGroupView> EnemyGroups;
    }
}