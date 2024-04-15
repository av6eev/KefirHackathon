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
            _view.OnEscaped += HandleEscapeInput;
            _view.OnAttack += HandleAttackInput;
            _view.OnDash += HandleDashInput;
            _view.OnAnyKey += HandleAnyKeyInput;

            _view.OnSkill1Toggled += HandleSkill1Input;
            _view.OnSkill2Toggled += HandleSkill2Input;
            _view.OnSkill3Toggled += HandleSkill3Input;
        }

        public void Dispose()
        {
            _view.Dispose();
            
            _view.OnMouseMove -= HandleMouseMoveInput;
            _view.OnMoved -= HandleMoveInput;
            _view.OnRun -= HandleRunInput;
            _view.OnInteracted -= HandleInteractInput;
            _view.OnEscaped -= HandleEscapeInput;
            _view.OnAttack -= HandleAttackInput;
            _view.OnDash -= HandleDashInput;
            _view.OnAnyKey -= HandleAnyKeyInput;

            _view.OnSkill1Toggled -= HandleSkill1Input;
            _view.OnSkill2Toggled -= HandleSkill2Input;
            _view.OnSkill3Toggled -= HandleSkill3Input;
        }

        private void HandleAnyKeyInput()
        {
            _model.AnyKeyInput();
        }

        private void HandleDashInput()
        {
            _model.Dash();
        }

        private void HandleAttackInput()
        {
            _model.Attack();
        }

        private void HandleRunInput(bool state)
        {
            _model.IsRun.Value = state;
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

        private void HandleSkillInput(int index)
        {
            _model.UseSkill(index);
        }

        private void HandleSkill1Input() => HandleSkillInput(0);
        private void HandleSkill2Input() => HandleSkillInput(1);
        private void HandleSkill3Input() => HandleSkillInput(2);
    }
}