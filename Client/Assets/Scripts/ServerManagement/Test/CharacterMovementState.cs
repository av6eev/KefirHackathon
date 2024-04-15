using Vector3 = UnityEngine.Vector3;

namespace ServerManagement.Test
{
    public struct CharacterMovementState
    {
        public int Tick;
        public Vector3 Position;
        public float RotationY;

        public CharacterMovementState(int tick, Vector3 position, float rotationY)
        {
            Tick = tick;
            Position = position;
            RotationY = rotationY;
        }
    }
}