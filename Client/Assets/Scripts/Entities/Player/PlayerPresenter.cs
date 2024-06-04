using System.Collections.Generic;
using Cameras;
using Entities.Player.Animator;
using Entities.Player.Physics;
using Entities.Player.Target;
using Loader.Object;
using Presenter;
using UnityEngine;
using Updater;

namespace Entities.Player
{
    public class PlayerPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly Transform _root;
        private PlayerView _view;

        private readonly PresentersList _presenters = new();
        private readonly List<IUpdater> _updaters = new();
        private ILoadObjectModel<GameObject> _loadObjectModel;

        public PlayerPresenter(GameModel gameModel, PlayerModel model, Transform root)
        {
            _gameModel = gameModel;
            _model = model;
            _root = root;
        }
        
        public async void Init()
        {
            _loadObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>("player");
            await _loadObjectModel.LoadAwaiter;

            var component = _loadObjectModel.Result.GetComponent<PlayerView>();
            _view = Object.Instantiate(component, _root);
            
            _gameModel.CameraModel.ChangeState(CameraStateType.PlayerFollow, _view.Root);
            
            _presenters.Add(new PlayerTargetPresenter(_gameModel, _model, _view));
            _presenters.Add(new PlayerAnimatorPresenter(_gameModel, _model, _view));
            _presenters.Add(new PlayerDashPresenter(_gameModel, _model, _view));
            _presenters.Add(new EnemyChangePresenter(_model));
            _presenters.Add(new PlayerKillCountChangePresenter(_model));
            _presenters.Add(new PlayerChangeLocationPresenter(_gameModel, _model));

            _updaters.Add(new PlayerPhysicsUpdater(_gameModel, _model, _view));
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