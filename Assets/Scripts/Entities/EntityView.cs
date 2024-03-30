using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public abstract class EntityView : MonoBehaviour, IEntityView
    {
        protected readonly int IsMovement = Animator.StringToHash("IsMovement");
        protected readonly int Speed = Animator.StringToHash("Speed");
        
        public Rigidbody Rigidbody;
        public Transform Root;
        public EntityType Type;
        public Image HealthBar;
        public GameObject WeaponSlot;
        public Animator EntityAnimatorController;
        public LayerMask EnemyLayer;

        public Vector3 Forward => Root.transform.forward;
        public Vector3 Right => Root.transform.right;
        public Vector3 Position => Root.transform.position;
        public Quaternion Rotation => Root.transform.rotation;
        public Vector3 LocalEulerAngles => Root.transform.localEulerAngles;

        public virtual void SetForward(Vector3 heading)
        {
            Root.transform.forward = heading;
        }

        public Vector3 TransformPoint(Vector3 offset)
        {
            return Root.transform.TransformPoint(offset);
        }

        public virtual void Move(Vector3 newPosition)
        {
            Root.position = newPosition;
        }

        public virtual void Rotate(Quaternion newRotation)
        {
            Root.rotation = newRotation;
        }
    }

    public interface IEntityView
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        Quaternion Rotation { get; }
        Vector3 LocalEulerAngles { get; }
        void SetForward(Vector3 heading);
        Vector3 TransformPoint(Vector3 cameraSpecificationOffset);
    }
}