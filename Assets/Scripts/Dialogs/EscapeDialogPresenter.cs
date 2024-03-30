using Dialogs.Collection;
using Presenter;

namespace Dialogs
{
    public class EscapeDialogPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly DialogsCollection _model;

        public EscapeDialogPresenter(IGameModel gameModel, DialogsCollection model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _gameModel.InputModel.OnEscaped += HandleEscape;
        }

        public void Dispose()
        {
            _gameModel.InputModel.OnEscaped -= HandleEscape;
        }

        private void HandleEscape()
        {
            if (!_model.IsEmpty)
            {
                _model.Last.Close();
            }
        }
    }
}