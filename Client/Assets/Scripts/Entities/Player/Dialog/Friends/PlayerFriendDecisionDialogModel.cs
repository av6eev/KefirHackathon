using Dialogs;
using Dialogs.Specification;

namespace Entities.Player.Dialog.Friends
{
    public class PlayerFriendDecisionDialogModel : DialogModel
    {
        public string OwnerNickname;
        public string InviteId;
        
        public PlayerFriendDecisionDialogModel(DialogSpecification specification) : base(specification)
        {
        }
    }
}