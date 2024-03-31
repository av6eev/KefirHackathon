using System;
using Reactive.Field;
using UnityEngine;

namespace Input
{
    public interface IInputModel
    {
        event Action OnInteracted;
        event Action OnEscaped;
        event Action<int, bool> OnSlotStateChanged;
        event Action OnAttack;
        event Action OnDash;
        event Action<int> OnSkillUse;
        event Action OnAnyKey;
        
        ReactiveField<bool> IsRun { get; }
        ReactiveField<Vector2> Direction { get; }
        Vector2 MouseDelta { get; }
        ReactiveField<int> CurrentActiveSlot { get; }
    }
}