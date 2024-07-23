using ServerCore.Main.Property;

namespace ServerCore.Main.Party
{
    public class PartyData : ServerData
    {
        public readonly Property<string> Guid = new("guid", string.Empty);
        public readonly Property<string> OwnerId = new("owner_id", string.Empty);
        public readonly Property<string> OwnerNickname = new("owner_nickname", string.Empty);
        public readonly Property<bool> InParty = new("in_party", false);

        public readonly DataList<string> Members = new("members");

        public PartyData() : base("party")
        {
            Properties.Add(Guid.Id, Guid);
            Properties.Add(OwnerId.Id, OwnerId);
            Properties.Add(OwnerNickname.Id, OwnerNickname);
            Properties.Add(InParty.Id, InParty);
        
            Dataset.Add(Members.Id, Members);
        }
    }
}