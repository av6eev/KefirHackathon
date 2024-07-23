
using Presenter;

namespace LoadingScreen
{
    public class LoadingScreenPresenter : IPresenter 
    {
        private readonly GameModel _gameModel;
        private readonly LoadingScreenModel _model;
        private readonly LoadingScreenView _view;
        
        private LoadingScreenUpdater _updater;

        public LoadingScreenPresenter(GameModel gameModel, LoadingScreenModel model, LoadingScreenView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            HandleStateChange(_model.IsShown.Value, false);

            _updater = new LoadingScreenUpdater(_model, _view);
            _gameModel.UpdatersList.Add(_updater);
            
            _model.IsShown.OnChanged += HandleStateChange;
            _model.CurrentScreenMessage.OnChanged += HandleMessageChange;
        }

        public void Dispose()
        {
            _gameModel.UpdatersList.Remove(_updater);
            
            _model.IsShown.OnChanged -= HandleStateChange;
            _model.CurrentScreenMessage.OnChanged -= HandleMessageChange;
        }

        private void HandleMessageChange(string newMessage, string oldMessage)
        {
            _view.MessageText.text = newMessage;
        }

        private void HandleStateChange(bool newValue, bool oldValue)
        {
            switch (newValue)
            {
                case true:
                    _view.Root.SetActive(true);
                    break;
                case false:
                    _view.Root.SetActive(false);
                    _view.ProgressFillImage.fillAmount = 0f;
                    
                    _model.Reset();
                    break;
            }
        }
    }
}