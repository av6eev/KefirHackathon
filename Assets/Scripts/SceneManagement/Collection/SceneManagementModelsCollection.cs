using Utilities.ModelCollection;

namespace SceneManagement.Collection
{
    public class SceneManagementModelsCollection : ModelCollection<SceneManagementModel>, ISceneManagementModelsCollection
    {
        public void Load(string id)
        {
            Add(new SceneManagementModel(id));
        }

        public void Unload(ISceneManagementModel model)
        {
            Remove((SceneManagementModel)model);
        }
    }

    public interface ISceneManagementModelsCollection : IModelCollection<SceneManagementModel>
    {
        void Load(string id);
        void Unload(ISceneManagementModel model);
    }
}