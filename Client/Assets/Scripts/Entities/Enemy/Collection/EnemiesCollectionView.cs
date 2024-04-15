using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Pull;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollectionView : MonoBehaviour
    {
        public Transform Root;
        public Transform SpawnZoneCenter;
        public List<Transform> EnemySpawnPositions;
        [NonSerialized] public Dictionary<string, GameObjectPull> EnemyPull = new();
    }
}