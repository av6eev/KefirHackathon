using System;
using System.Collections.Generic;
using Reactive.Field;
using UnityEngine;

namespace Input
{
    public class InputModel : IInputModel
    {
        public event Action OnInteracted;
        public event Action OnEscaped;
        public event Action<int, bool> OnSlotStateChanged;
        public event Action OnAttack;
        public event Action OnDash;
        public event Action<int> OnSkillUse;
        public event Action OnAnyKey;
        public event Action OnDebugPanelToggle;

        public Vector2 MouseDelta { get; set; }
        public Vector2 MousePosition { get; set; }
        public ReactiveField<Vector2> Direction { get; } = new();
        public ReactiveField<bool> IsRun { get; } = new();
        private readonly Dictionary<int, bool> _slotStates = new();
        public ReactiveField<int> CurrentActiveSlot { get; } = new (-1);

        public void SetSlotState(int index)
        {
            if (CurrentActiveSlot.Value != -1)
            {
                _slotStates[CurrentActiveSlot.Value] = false;
            }
            
            if (_slotStates.TryGetValue(index, out var oldState))
            {
                _slotStates[index] = !oldState;
            }
            else
            {
                _slotStates.Add(index, true);
            }

            CurrentActiveSlot.Value = index;
            
            OnSlotStateChanged?.Invoke(index, _slotStates[index]);
        }

        public void UseSkill(int index)
        {
            OnSkillUse?.Invoke(index);
        }
        
        public void Attack()
        {
            OnAttack?.Invoke();;
        }
        
        public void Interact()
        {
            OnInteracted?.Invoke();
        }

        public void Escape()
        {
            OnEscaped?.Invoke();
        }

        public void Dash()
        {
            OnDash?.Invoke();
        }

        public void AnyKeyInput()
        {
            OnAnyKey?.Invoke();
        }

        public void ToggleDebugPanel()
        {
            OnDebugPanelToggle?.Invoke();
        }
    }
}