using System;
using UnityEngine;

namespace Entities.Enemy.Collection
{
    [Serializable]
    public class EnemyGroupView
    {
        public string GroupId;
        public float SpawnRange;
        public GameObject Root;
    }
}