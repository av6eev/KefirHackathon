using Cameras;
using Updater;

namespace InteractiveObjects
{
    public class InteractiveObjectUpdater : IUpdater
    {
        private readonly InteractiveObjectView _view;
        private readonly ICameraModel _cameraModel;

        public InteractiveObjectUpdater(InteractiveObjectView view, ICameraModel cameraModel)
        {
            _view = view;
            _cameraModel = cameraModel;
        }
        
        public void Update(float deltaTime)
        {
            if (!_view.IsInRange)
            {
                return;
            }
            
            _view.Tooltip.transform.LookAt(_cameraModel.Position);
        }
    }
}