using Loader.Scene;
using Utilities.Model;

namespace SceneManagement
{
    public interface ISceneManagementModel : ILoadSceneModel, IModel
    {
        string SceneId { get; } 
    }
}