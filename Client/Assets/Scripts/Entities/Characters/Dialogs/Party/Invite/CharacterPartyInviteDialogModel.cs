using Dialogs;
using Dialogs.Specification;

namespace Entities.Characters.Dialogs.Party.Invite
{
    public class CharacterPartyInviteDialogModel : DialogModel
    {
        public string InvitedUserId;

        public CharacterPartyInviteDialogModel(DialogSpecification specification) : base(specification)
        {
        }
    }
}