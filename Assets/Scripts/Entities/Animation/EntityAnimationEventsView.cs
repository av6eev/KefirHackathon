using System;
using UnityEngine;

namespace Entities.Animation
{
    public class EntityAnimationEventsView : MonoBehaviour
    {
        public event Action OnEndRoll;
        public event Action OnEndAttack;
        public event Action OnNeedEffect;
        public event Action OnSimpleAttack;
        public event Action OnEndSimpleAttack;
        public event Action OnDeath;
        
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

        public void SimpleAttack()
        {
            OnSimpleAttack?.Invoke();
        }

        public void EndSimpleAttack()
        {
            OnEndSimpleAttack?.Invoke();
        }

        public void Death()
        {
            OnDeath?.Invoke();
        }
    }
}