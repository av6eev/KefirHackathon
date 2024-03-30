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

        public ReactiveField<bool> IsRun { get; } = new();
        public ReactiveField<Vector2> Direction { get; } = new();
        public Vector2 MouseDelta { get; set; }

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
    }
}