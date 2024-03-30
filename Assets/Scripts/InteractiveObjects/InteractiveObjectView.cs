using System;
using Entities.Player;
using UnityEngine;

namespace InteractiveObjects
{
    public class InteractiveObjectView : MonoBehaviour
    {
        public event Action OnInRange;
        public event Action OnOutOfRange;

        public bool IsInRange;
        public GameObject Tooltip;
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(PlayerModel.Id))
            {
                IsInRange = true;
                Tooltip.SetActive(true);
                
                OnInRange?.Invoke();
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(PlayerModel.Id))
            {
                IsInRange = false;
                Tooltip.SetActive(false);

                OnOutOfRange?.Invoke();
            }
        }
    }
}