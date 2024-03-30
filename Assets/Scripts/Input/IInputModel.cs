using System;
using Reactive.Field;
using UnityEngine;

namespace Input
{
    public interface IInputModel
    {
        event Action OnInventoryOpened;
        event Action OnInteracted;
        event Action OnEscaped;
        event Action<int, bool> OnSlotStateChanged;
        event Action OnAttack;
        
        bool IsLook { get; }
        ReactiveField<bool> IsRun { get; }
        ReactiveField<Vector2> Direction { get; }
        Vector2 MouseDelta { get; }
    }
}