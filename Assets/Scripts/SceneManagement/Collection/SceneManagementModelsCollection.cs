using Utilities.ModelCollection;

namespace SceneManagement.Collection
{
    public class SceneManagementModelsCollection : ModelCollection<string, SceneManagementModel>, ISceneManagementModelsCollection
    {
        public string CurrentSceneId { get; set; }
        
        public void Load(string id)
        {
            Add(id, new SceneManagementModel(id));
        }

        public void Unload(string id)
        {
            Remove(id);
        }

        public void SetCurrentSceneId(string id)
        {
            CurrentSceneId = id;
        }
    }
}