using Entities;
using UnityEngine;

namespace Cameras
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        public Transform Root;
        private Camera _camera;
        
        private EntityView _target;
        public IEntityView Target
        {
            get => _target;
            private set => _target = (EntityView)value;
        }
        
        public Vector3 Position => Root.position;
        public Quaternion Rotation => Root.rotation;
        public Vector3 LocalEulerAngles => Root.localEulerAngles;
        public Vector3 Forward => Root.forward;
        public Vector3 Right => Root.right;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void SetTarget(IEntityView target)
        {
            Target = target;
        }

        public void Rotate(Quaternion newRotation)
        {
            Root.rotation = newRotation;
        }
        
        public void Rotate(Vector3 newEulerAngle)
        {
            Root.localEulerAngles = newEulerAngle;
        }

        public void Follow(Vector3 newPosition)
        {
            Root.position = newPosition;
        }
        
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}