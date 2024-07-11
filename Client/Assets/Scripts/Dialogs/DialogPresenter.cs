using Dialogs.Collection;
using Loader.Object;
using Presenter;
using UnityEngine;

namespace Dialogs
{
    public abstract class DialogPresenter<T> : IPresenter where T : DialogView
    {
        private readonly IGameModel _gameModel;
        private readonly DialogModel _model;
        private readonly DialogsCollectionView _collectionView;
        
        protected T View;
        private ILoadObjectModel<GameObject> _loadObjectModel;

        protected DialogPresenter(IGameModel gameModel, DialogModel model, DialogsCollectionView collectionView)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
        }
        
        public async void Init()
        {
            _model.OnClose += HandleClose;
            
            _loadObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>(_model.Specification.PrefabId);
            await _loadObjectModel.LoadAwaiter;

            var component = _loadObjectModel.Result.GetComponent<T>();
            View = Object.Instantiate(component, _collectionView.ContentRoot);
            
            View.ExitButton.onClick.AddListener(HandleClose);
            AfterInit();
        }

        public void Dispose()
        {
            _model.OnClose -= HandleClose;
            View.ExitButton.onClick.RemoveListener(HandleClose);

            Object.Destroy(View.gameObject);
            _gameModel.LoadObjectsModel.Unload(_loadObjectModel);
            
            AfterDispose();
        }

        protected void HandleClose()
        {
            _model.IsOpened = false;
            _gameModel.DialogsCollection.Remove(_model);
        }

        protected abstract void AfterInit();
        protected abstract void AfterDispose();
    }
}