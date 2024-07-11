using Server.Party.Invite;
using ServerCore.Main.Party;
using ServerCore.Main.Utilities.Logger;

namespace Server.Party;

public class PartyModel
{
    public event Action<string> OnMemberAdded; 
    public event Action<string> OnMemberRemoved; 
    
    public readonly string Guid;
    public readonly string OwnerId;
    public readonly string OwnerNickname;
    
    public readonly Dictionary<string, PartyInviteModel> Invites = new();
    public readonly List<string> Members = new();
    
    public PartyModel(string guid, string ownerId, string ownerNickname)
    {
        Guid = guid;
        OwnerId = ownerId;
        OwnerNickname = ownerNickname;
    }

    public void AddMember(string memberNickname)
    {
        Members.Add(memberNickname);
        
        OnMemberAdded?.Invoke(memberNickname);
    }
    
    public void RemoveMember(string memberNickname)
    {
        if (!Members.Remove(memberNickname))
        {
            Logger.Instance.Log($"Error while removing user with nickname: {memberNickname} from party: {Guid}");
            return;
        }
        
        OnMemberRemoved?.Invoke(memberNickname);
    }
}