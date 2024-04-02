using System;

namespace Entities.Animation
{
    public class EntityAnimationEvents
    {
        public event Action OnEndRoll;
        public event Action OnEndAttack;
        public event Action OnNeedEffect;
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

        public void Death()
        {
            OnDeath?.Invoke();
        }
    }
}