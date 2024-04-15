using UnityEngine;

namespace Entities.Enemy
{
    public class FireBallView : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Vector3 Direction;
        public float Speed;
        public float _lifeTime = 3f;
        public float _currentLifeTime = 3f;
        
        private void Update()
        {
            Rigidbody.AddRelativeForce(Direction, ForceMode.Impulse);

            if (_currentLifeTime < _lifeTime)
            {
                _currentLifeTime++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}