using Dialogs;
using Dialogs.Specification.Panel;

namespace Entities.Characters.Dialogs.Select.Actions
{
    public class CharacterSelectActionsDialogModel : DialogModel
    {
        public string SelectedUserId;

        public CharacterSelectActionsDialogModel(PanelDialogSpecification specification) : base(specification)
        {
            
        }
    }
}