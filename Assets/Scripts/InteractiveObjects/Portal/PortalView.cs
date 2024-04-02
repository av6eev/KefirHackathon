using UnityEngine;

namespace InteractiveObjects.Portal
{
    public class PortalView : MonoBehaviour
    {
        public InteractiveObjectView InteractiveObject;
        public string NextSceneId;
        public string FromSceneId;
        public MeshRenderer MeshRenderer;
        public Collider MeshCollider;
    }
}