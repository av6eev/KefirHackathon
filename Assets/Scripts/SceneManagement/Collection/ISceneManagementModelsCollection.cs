using Utilities.ModelCollection;

namespace SceneManagement.Collection
{
    public interface ISceneManagementModelsCollection : IModelCollection<string, SceneManagementModel>
    {
        string CurrentSceneId { get; }
        void Load(string id);
        void Unload(string id);
        void SetCurrentSceneId(string id);
    }
}