using System.Collections.ObjectModel;
using Server.Utilities.ModelCollection;

namespace Server.Save.Single.Collection
{
    public class SaveSingleModelCollection : ModelCollection<SaveSingleModel>, ISaveSingleModelCollection
    {
        public void Add(INotifySaveModel model)
        {
            Add(new SaveSingleModel(model));
        }

        public void RemoveElement(string saveId)
        {
            var element = Collection.Find(element => element.SaveModel.SaveId == saveId);
            Remove(element);
        }
    }
}