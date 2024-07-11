using Dialogs;
using Dialogs.Specification;

namespace Entities.Player.Dialog.Party
{
    public class PlayerPartyDecisionDialogModel : DialogModel
    {
        public string OwnerNickname;
        public string InviteId;

        public PlayerPartyDecisionDialogModel(DialogSpecification specification) : base(specification)
        {
        }
    }
}