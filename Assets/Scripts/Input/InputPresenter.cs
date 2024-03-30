using Presenter;
using UnityEngine;

namespace Input
{
    public class InputPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly InputModel _model;
        private readonly InputView _view;

        public InputPresenter(IGameModel gameModel, InputModel model, InputView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }

        public void Init()
        {
            _view.Initialize();
            
            _view.OnMouseMove += HandleMouseMoveInput;
            _view.OnMoved += HandleMoveInput;
            _view.OnRun += HandleRunInput;
            _view.OnInteracted += HandleInteractInput;
            _view.OnInventoryToggled += HandleInventoryInput;
            _view.OnEscaped += HandleEscapeInput;
            _view.OnLook += HandleLookInput;
            _view.OnAttack += HandleAttackInput;

            _view.OnSlot1Toggled += HandleSlot1Input;
            _view.OnSlot2Toggled += HandleSlot2Input;
            _view.OnSlot3Toggled += HandleSlot3Input;
            _view.OnSlot4Toggled += HandleSlot4Input;
        }

        public void Dispose()
        {
            _view.Dispose();
            
            _view.OnMouseMove -= HandleMouseMoveInput;
            _view.OnMoved -= HandleMoveInput;
            _view.OnRun -= HandleRunInput;
            _view.OnInteracted -= HandleInteractInput;
            _view.OnInventoryToggled -= HandleInventoryInput;
            _view.OnEscaped -= HandleEscapeInput;
            _view.OnLook -= HandleLookInput;
            _view.OnAttack -= HandleAttackInput;

            _view.OnSlot1Toggled -= HandleSlot1Input;
            _view.OnSlot2Toggled -= HandleSlot2Input;
            _view.OnSlot3Toggled -= HandleSlot3Input;
            _view.OnSlot4Toggled -= HandleSlot4Input;
        }

        private void HandleAttackInput()
        {
            _model.Attack();
        }

        private void HandleLookInput(bool state)
        {
            _model.IsLook = state;
        }

        private void HandleRunInput(bool state)
        {
            _model.IsRun.Value = state;
        }

        private void HandleInventoryInput()
        {
            _model.OpenInventory();
        }

        private void HandleEscapeInput()
        {
            _model.Escape();
        }

        private void HandleInteractInput()
        {
            _model.Interact();
        }

        private void HandleMoveInput(Vector2 input)
        {
            _model.Direction.Value = input;
        }

        private void HandleMouseMoveInput(Vector2 input)
        {
            _model.MouseDelta = input;
        }
        
        private void HandleStateChange(int index)
        {
            _model.SetSlotState(index);
        }
        
        private void HandleSlot1Input() => HandleStateChange(0);
        private void HandleSlot2Input() => HandleStateChange(1);
        private void HandleSlot3Input() => HandleStateChange(2);
        private void HandleSlot4Input() => HandleStateChange(3);
    }
}