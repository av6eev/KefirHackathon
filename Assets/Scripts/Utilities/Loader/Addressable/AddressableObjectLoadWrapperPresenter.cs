using Presenter;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utilities.Loader.Addressable
{
    public class AddressableObjectLoadWrapperPresenter<T> : IPresenter
    {
        private readonly AddressableObjectLoadWrapperModel<T> _model;
        private AsyncOperationHandle<T> _asyncOperation;

        public AddressableObjectLoadWrapperPresenter(AddressableObjectLoadWrapperModel<T> model)
        {
            _model = model;
        }
        
        public void Init()
        {
            _asyncOperation = Addressables.LoadAssetAsync<T>(_model.LoadObjectToWrapperModel.Key);

            _asyncOperation.Completed += OnCompleted;
        }

        public void Dispose()
        {
            _asyncOperation.Completed -= OnCompleted;
            
            Addressables.Release(_asyncOperation);
            _model.LoadObjectToWrapperModel.CompleteUnload();
        }

        private void OnCompleted(AsyncOperationHandle<T> operation)
        {
            _model.LoadObjectToWrapperModel.Result = operation.Result;
            _model.LoadObjectToWrapperModel.CompleteLoad();
        }
    }
}