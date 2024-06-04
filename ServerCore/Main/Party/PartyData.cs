using ServerCore.Main.Property;
using ServerCore.Main.Users;

namespace ServerCore.Main.Party
{
    public class PartyData : ServerData
    {
        public const int MaxMemberCount = 3;

        public readonly Property<string> OwnerId = new("owner_id", string.Empty);
        public readonly Property<bool> InParty = new("in_party", false);

        public readonly DataList<string> Members = new("members");
        public readonly DataCollection<string, PartyInviteData> Invites = new("invites");

        public PartyData(string id) : base(id)
        {
            Properties.Add(OwnerId.Id, OwnerId);
            Properties.Add(InParty.Id, InParty);
        
            Dataset.Add(Members.Id, Members);
            Dataset.Add(Invites.Id, Invites);
        }
    }
}