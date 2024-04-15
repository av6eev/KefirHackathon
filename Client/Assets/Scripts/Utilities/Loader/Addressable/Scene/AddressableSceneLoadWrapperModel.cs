using Loader.Scene;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Utilities.Loader.Addressable.Scene
{
    public class AddressableSceneLoadWrapperModel : IAddressableSceneLoadWrapperModel
    {
        public ILoadSceneToWrapperModel LoadObjectToWrapperModel { get; }
        
        public AsyncOperationHandle<SceneInstance> LoadAsyncOperation;
        public AsyncOperationHandle<SceneInstance> UnloadAsyncOperation;
        
        public AddressableSceneLoadWrapperModel(ILoadSceneToWrapperModel loadObjectToWrapperModel)
        {
            LoadObjectToWrapperModel = loadObjectToWrapperModel;
        }
    }

    public interface IAddressableSceneLoadWrapperModel
    {
    }
}