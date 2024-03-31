using UnityEngine;

namespace Entities.Enemy
{
    public class FireBallView : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Vector3 Direction;
        public float Speed;
        
        private void Update()
        {
            Rigidbody.AddRelativeForce(Direction, ForceMode.Impulse);
        }
    }
}