using Utilities.ModelCollection;

namespace Save.Single.Collection
{
    public class SaveSingleModelCollection : ModelCollection<SaveSingleModel>, ISaveSingleModelCollection
    {
        public void Add(INotifySaveModel model)
        {
            Add(new SaveSingleModel(model));
        }
    }
}