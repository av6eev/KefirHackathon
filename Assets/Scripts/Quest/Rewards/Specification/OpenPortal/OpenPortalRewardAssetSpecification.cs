using System;
using Entities.Player;
using InteractiveObjects.Portal;
using UnityEngine;

namespace Quest.Rewards.Specification.OpenPortal
{
    [Serializable]
    public class OpenPortalRewardAssetSpecification : BaseRewardSpecification
    {
        public string Name;
        
        public override void Give(IGameModel gameModel)
        {
            var portalView = GameObject.Find(Name).GetComponent<PortalView>();
            portalView.MeshCollider.enabled = true;
            portalView.MeshRenderer.enabled = true;
            
            Debug.Log(portalView);
        }
    }
}