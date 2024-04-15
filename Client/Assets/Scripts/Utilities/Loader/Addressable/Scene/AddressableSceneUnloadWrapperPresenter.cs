using Presenter;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Utilities.Loader.Addressable.Scene
{
    public class AddressableSceneUnloadWrapperPresenter : IPresenter
    {
        private readonly AddressableSceneLoadWrapperModel _model;

        public AddressableSceneUnloadWrapperPresenter(AddressableSceneLoadWrapperModel model)
        {
            _model = model;
        }
        
        public void Init()
        {
            _model.UnloadAsyncOperation = Addressables.UnloadSceneAsync(_model.LoadAsyncOperation);
            _model.UnloadAsyncOperation.Completed += OnCompleted;
        }

        public void Dispose()
        {
            _model.UnloadAsyncOperation.Completed -= OnCompleted;
        }

        private void OnCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            _model.LoadObjectToWrapperModel.CompleteUnload();
        }
    }
}