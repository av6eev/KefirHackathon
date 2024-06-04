using Entities.Characters.Animator;
using Entities.Characters.Physics;
using Entities.Player;
using Loader.Object;
using Presenter;
using UnityEngine;
using Updater;

namespace Entities.Characters
{
    public class CharacterPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly CharacterModel _model;
        private readonly Transform _root;
        private PlayerView _view;

        private readonly PresentersList _presenters = new();
        private ILoadObjectModel<GameObject> _loadObjectModel;
        private IUpdater _updater;

        public CharacterPresenter(IGameModel gameModel, CharacterModel model, Transform root)
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
            _view.gameObject.name = _model.ServerData.PlayerId.Value;
            
            _presenters.Add(new CharacterAnimatorPresenter(_gameModel, _model, _view));
            _presenters.Init();
            
            _updater = new CharacterPhysicsUpdater(_model, _view);
            _gameModel.UpdatersList.Add(_updater);
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();

            _gameModel.UpdatersList.Remove(_updater);

            Object.Destroy(_view.gameObject);
            _gameModel.LoadObjectsModel.Unload(_loadObjectModel);

            Debug.Log("disposed");
        }
    }
}