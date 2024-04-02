using Updater;

namespace Entities.Player.Dialog
{
    public class PlayerDialogUpdater : IUpdater
    {
        private readonly PlayerDialogModel _model;
        private readonly PlayerDialogView _view;

        private bool _isShow;
        private float _showTime = 2f;
        private float _currentShowTime = 0f;
        
        public PlayerDialogUpdater(PlayerDialogModel model, PlayerDialogView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            if (_isShow)
            {
                if (_currentShowTime >= _showTime)
                {
                    if (_model.Queue.Count > 0)
                    {
                        var text = _model.Queue.Dequeue();
                        
                        _view.Text.text = text;
                        
                        _currentShowTime = 0;
                    }
                    else
                    {
                        _isShow = false;
                        _currentShowTime = 0;
                        _view.Root.SetActive(false);
                    }
                }
                else
                {
                    _currentShowTime += deltaTime;
                }
            }
            else
            {
                if (_model.Queue.Count > 0)
                {
                    var text = _model.Queue.Dequeue();
                        
                    _view.Text.text = text;
                        
                    _currentShowTime = 0;
                    _view.Root.SetActive(true);
                    _isShow = true;
                }
            }
        }
    }
}