using Entities.Enemy;
using UnityEngine;

namespace Entities.Player.Target
{
    public static class RaycastHelper
    {
        public static bool IsRaycastTarget(Vector3 startPosition, Vector3 target, out RaycastHit hit, float angleVisionDistance, LayerMask enemyLayer, float y = 1)
        {
            var startRay = new Vector3(startPosition.x, y, startPosition.z);
            var deltaVector = new Vector3(target.x, 1, target.z) - startRay;
            var ray = new Ray(startRay, deltaVector.normalized);

            if (!UnityEngine.Physics.Raycast(ray, out hit, angleVisionDistance, enemyLayer)) return false;
            
            if (!hit.collider.transform.parent.TryGetComponent<EnemyView>(out var enemyView)) return false;
                
            return enemyView.Type == EntityType.Enemy;
        }
    }
}