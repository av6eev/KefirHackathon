using Dialogs;
using Dialogs.Specification;

namespace Entities.Characters.Dialogs.Panel
{
    public class CharacterSelectDialogModel : DialogModel
    {
        public string SelectedUserId;
        
        public CharacterSelectDialogModel(DialogSpecification specification) : base(specification)
        {
        }
    }
}