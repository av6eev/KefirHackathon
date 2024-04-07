using Entities;
using UnityEngine;

namespace Cameras
{
    public interface ICameraView
    {
        IEntityView Target { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        void SetTarget(IEntityView target);
        void Follow(Vector3 newPosition);
        void Rotate(Quaternion newRotation);
        void Show();
        void Hide();
    }
}