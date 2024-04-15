using Utilities.ModelCollection;

namespace Dialogs.Collection
{
    public interface IDialogsCollection : IModelCollection<DialogModel>
    {
        DialogModel Last { get; }
        void AddDialog(DialogModel model);
    }
}