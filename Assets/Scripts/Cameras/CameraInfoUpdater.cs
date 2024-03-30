using Updater;

namespace Cameras
{
    public class CameraInfoUpdater : IUpdater
    {
        private readonly CameraModel _model;
        private readonly CameraView _view;

        public CameraInfoUpdater(CameraModel model, CameraView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            _model.CurrentEulerAngles = _view.LocalEulerAngles;
            _model.Forward = _view.Forward;
            _model.Right = _view.Right;
            _model.Position = _view.Position;
        }
    }
}