using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputView : MonoBehaviour
    {
        public event Action<Vector2> OnMoved; 
        public event Action<Vector2> OnMouseMove;
        public event Action<Vector2> OnMousePositionChange;
        public event Action OnInteracted;
        public event Action OnEscaped;
        public event Action<bool> OnRun; 
        public event Action OnAttack; 
        public event Action OnDash; 
        public event Action OnSkill1Toggled; 
        public event Action OnSkill2Toggled; 
        public event Action OnSkill3Toggled;
        public event Action OnAnyKey;
        public event Action OnDebugPanelToggled;

        public InputActionAsset PlayerInputAsset;

        public void Initialize()
        {
            PlayerInputAsset.Enable();
            
            PlayerInputAsset["Movement"].performed += OnMoveInput;
            PlayerInputAsset["MouseDelta"].performed += OnMouseMoveInput;
            PlayerInputAsset["MousePosition"].performed += OnMousePositionInput;
            PlayerInputAsset["Interact"].performed += OnInteractPerformedInput;
            PlayerInputAsset["Escape"].performed += OnEscapeInput;
            PlayerInputAsset["Run"].performed += OnRunInputPerformed;
            PlayerInputAsset["Run"].canceled += OnRunInputCanceled;
            PlayerInputAsset["Attack"].performed += OnAttackInput;
            PlayerInputAsset["Dash"].performed += OnDashInput;
            PlayerInputAsset["Any"].performed += OnAnyKeyInput;
            PlayerInputAsset["DebugPanel"].performed += OnDebugPanelInput;
            
            PlayerInputAsset["Skill1"].performed += OnSkill1InputPerformed;
            PlayerInputAsset["Skill2"].performed += OnSkill2InputPerformed;
            PlayerInputAsset["Skill3"].performed += OnSkill3InputPerformed;
        }

        public void Dispose()
        {
            PlayerInputAsset.Disable();
            
            PlayerInputAsset["Movement"].performed -= OnMoveInput;
            PlayerInputAsset["MouseDelta"].performed -= OnMouseMoveInput;
            PlayerInputAsset["MousePosition"].performed -= OnMousePositionInput;
            PlayerInputAsset["Interact"].performed -= OnInteractPerformedInput;
            PlayerInputAsset["Escape"].performed -= OnEscapeInput;
            PlayerInputAsset["Run"].performed -= OnRunInputPerformed;
            PlayerInputAsset["Run"].canceled -= OnRunInputCanceled;
            PlayerInputAsset["Attack"].performed -= OnAttackInput;
            PlayerInputAsset["Dash"].performed -= OnDashInput;
            PlayerInputAsset["Any"].performed -= OnAnyKeyInput;
            PlayerInputAsset["DebugPanel"].performed -= OnDebugPanelInput;

            PlayerInputAsset["Skill1"].performed -= OnSkill1InputPerformed;
            PlayerInputAsset["Skill2"].performed -= OnSkill2InputPerformed;
            PlayerInputAsset["Skill3"].performed -= OnSkill3InputPerformed;
        }

        private void OnDebugPanelInput(InputAction.CallbackContext ctx) => OnDebugPanelToggled?.Invoke();
        private void OnMousePositionInput(InputAction.CallbackContext ctx) => OnMousePositionChange?.Invoke(ctx.ReadValue<Vector2>());
        private void OnAnyKeyInput(InputAction.CallbackContext ctx) => OnAnyKey?.Invoke();
        private void OnDashInput(InputAction.CallbackContext ctx) => OnDash?.Invoke();
        private void OnAttackInput(InputAction.CallbackContext ctx) => OnAttack?.Invoke();
        private void OnRunInputPerformed(InputAction.CallbackContext ctx) => OnRun?.Invoke(true);
        private void OnRunInputCanceled(InputAction.CallbackContext ctx) => OnRun?.Invoke(false);
        private void OnEscapeInput(InputAction.CallbackContext ctx) => OnEscaped?.Invoke();
        private void OnInteractPerformedInput(InputAction.CallbackContext ctx) => OnInteracted?.Invoke();
        private void OnMoveInput(InputAction.CallbackContext ctx) => OnMoved?.Invoke(ctx.ReadValue<Vector2>());
        private void OnMouseMoveInput(InputAction.CallbackContext ctx) => OnMouseMove?.Invoke(ctx.ReadValue<Vector2>());

        private void OnSkill1InputPerformed(InputAction.CallbackContext ctx) => OnSkill1Toggled?.Invoke();
        private void OnSkill2InputPerformed(InputAction.CallbackContext ctx) => OnSkill2Toggled?.Invoke();
        private void OnSkill3InputPerformed(InputAction.CallbackContext ctx) => OnSkill3Toggled?.Invoke();
    }
}