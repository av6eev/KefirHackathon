using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputView : MonoBehaviour
    {
        public event Action<Vector2> OnMoved; 
        public event Action<Vector2> OnMouseMove;
        public event Action OnInteracted;
        public event Action OnEscaped;
        public event Action<bool> OnRun; 
        public event Action OnAttack; 
        public event Action OnSlot1Toggled; 
        public event Action OnSlot2Toggled; 
        public event Action OnSlot3Toggled; 
        public event Action OnSlot4Toggled; 

        public InputActionAsset PlayerInputAsset;

        public void Initialize()
        {
            PlayerInputAsset.Enable();
            
            PlayerInputAsset["Movement"].performed += OnMoveInput;
            PlayerInputAsset["MouseDelta"].performed += OnMouseMoveInput;
            PlayerInputAsset["Interact"].performed += OnInteractPerformedInput;
            PlayerInputAsset["Escape"].performed += OnEscapeInput;
            PlayerInputAsset["Run"].performed += OnRunInputPerformed;
            PlayerInputAsset["Run"].canceled += OnRunInputCanceled;
            PlayerInputAsset["Attack"].performed += OnAttackInput;
            
            PlayerInputAsset["Slot1"].performed += OnSlot1InputPerformed;
            PlayerInputAsset["Slot2"].performed += OnSlot2InputPerformed;
            PlayerInputAsset["Slot3"].performed += OnSlot3InputPerformed;
            PlayerInputAsset["Slot4"].performed += OnSlot4InputPerformed;
        }

        public void Dispose()
        {
            PlayerInputAsset.Disable();
            
            PlayerInputAsset["Movement"].performed -= OnMoveInput;
            PlayerInputAsset["MouseDelta"].performed -= OnMouseMoveInput;
            PlayerInputAsset["Interact"].performed -= OnInteractPerformedInput;
            PlayerInputAsset["Escape"].performed -= OnEscapeInput;
            PlayerInputAsset["Run"].performed -= OnRunInputPerformed;
            PlayerInputAsset["Run"].canceled -= OnRunInputCanceled;
            PlayerInputAsset["Attack"].performed -= OnAttackInput;
            
            PlayerInputAsset["Slot1"].performed -= OnSlot1InputPerformed;
            PlayerInputAsset["Slot2"].performed -= OnSlot2InputPerformed;
            PlayerInputAsset["Slot3"].performed -= OnSlot3InputPerformed;
            PlayerInputAsset["Slot4"].performed -= OnSlot4InputPerformed;
        }

        private void OnAttackInput(InputAction.CallbackContext ctx) => OnAttack?.Invoke();
        private void OnRunInputPerformed(InputAction.CallbackContext ctx) => OnRun?.Invoke(true);
        private void OnRunInputCanceled(InputAction.CallbackContext ctx) => OnRun?.Invoke(false);
        private void OnEscapeInput(InputAction.CallbackContext ctx) => OnEscaped?.Invoke();
        private void OnInteractPerformedInput(InputAction.CallbackContext ctx) => OnInteracted?.Invoke();
        private void OnMoveInput(InputAction.CallbackContext ctx) => OnMoved?.Invoke(ctx.ReadValue<Vector2>());
        private void OnMouseMoveInput(InputAction.CallbackContext ctx) => OnMouseMove?.Invoke(ctx.ReadValue<Vector2>());

        private void OnSlot1InputPerformed(InputAction.CallbackContext ctx) => OnSlot1Toggled?.Invoke();
        private void OnSlot2InputPerformed(InputAction.CallbackContext ctx) => OnSlot2Toggled?.Invoke();
        private void OnSlot3InputPerformed(InputAction.CallbackContext ctx) => OnSlot3Toggled?.Invoke();
        private void OnSlot4InputPerformed(InputAction.CallbackContext ctx) => OnSlot4Toggled?.Invoke();
    }
}