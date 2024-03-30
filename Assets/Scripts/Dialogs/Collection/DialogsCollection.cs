using System.Linq;
using Utilities.ModelCollection;

namespace Dialogs.Collection
{
    public class DialogsCollection : ModelCollection<DialogModel>, IDialogsCollection
    {
        public DialogModel Last => Collection.Last();
        
        public void AddDialog(DialogModel model)
        {
            model.IsOpened = true;
            Add(model);
        }
    }
}