using System.Collections.Generic;
using Entities.Player.Animator;
using Entities.Player.Physics;
using Presenter;
using Updater;

namespace Entities.Player
{
    public class PlayerPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        private readonly PresentersList _presenters = new();
        private readonly List<IUpdater> _updaters = new();
        
        public PlayerPresenter(GameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _presenters.Add(new PlayerAnimatorPresenter(_gameModel, _model, _view));
            _presenters.Add(new PlayerDashPresenter(_gameModel, _model, _view));
            
            _updaters.Add(new PlayerPhysicsUpdater(_gameModel.InputModel, _model, _view, _gameModel.CameraModel));
            _updaters.Add(new PlayerInfoUpdater(_model, _view));
            
            _presenters.Init();

            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Add(updater);
            }
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
            
            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Remove(updater);
            }
            
            _updaters.Clear();
        }
    }
}