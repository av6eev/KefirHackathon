using Utilities.ModelCollection;

namespace SceneManagement.Collection
{
    public interface ISceneManagementModelsCollection : IModelCollection<string, SceneManagementModel>
    {
        void Load(string id);
        void Unload(string id);
    }
}