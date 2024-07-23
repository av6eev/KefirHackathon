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
        event Action OnDebugPanelToggle;
        event Action OnFriendsPanelToggle;

        ReactiveField<bool> IsRun { get; }
        ReactiveField<Vector2> Direction { get; }
        Vector2 MouseDelta { get; }
        Vector2 MousePosition { get; }
        ReactiveField<int> CurrentActiveSlot { get; }
        
        void Enable();
        void Disable();
    }
}