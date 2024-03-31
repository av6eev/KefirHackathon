using System;
using UnityEngine;

namespace Entities.Animation
{
    public class EntityAnimationEventsView : MonoBehaviour
    {
        public event Action OnEndRoll;
        public event Action OnEndAttack;
        public event Action OnNeedEffect;
        
        public void EndRoll()
        {
            OnEndRoll?.Invoke();
        }

        public void EndAttack()
        {
            OnEndAttack?.Invoke();
        }

        public void NeedEffect()
        {
            OnNeedEffect?.Invoke();
        }
    }
}