using System;
using UnityEngine;

namespace Entities.Player.Animator
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public event Action OnEndRoll;
        
        public void EndRoll()
        {
            OnEndRoll?.Invoke();
        }
    }
}