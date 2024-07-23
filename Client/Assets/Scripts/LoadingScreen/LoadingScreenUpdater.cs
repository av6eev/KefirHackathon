using UnityEngine;
using Updater;

namespace LoadingScreen
{
    public class LoadingScreenUpdater : IUpdater
    {
        private const float LerpSpeed = 0.9f;
        
        private readonly LoadingScreenModel _model;
        private readonly LoadingScreenView _view;

        public LoadingScreenUpdater(LoadingScreenModel model, LoadingScreenView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            if (!_model.IsShown.Value) return;
            if (_model.CurrentProgress == 0) return;
            
            _view.ProgressFillImage.fillAmount = Mathf.Lerp(_view.ProgressFillImage.fillAmount, _model.CurrentProgress / _model.MaxLoadElementsCount, LerpSpeed * deltaTime);
        }
    }
}