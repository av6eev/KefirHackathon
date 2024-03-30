using Utilities.ModelCollection;

namespace SceneManagement.Collection
{
    public class SceneManagementModelsCollection : ModelCollection<string, SceneManagementModel>, ISceneManagementModelsCollection
    {
        public void Load(string id)
        {
            Add(id, new SceneManagementModel(id));
        }

        public void Unload(string id)
        {
            Remove(id);
        }
    }
}