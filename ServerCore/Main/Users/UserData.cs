using ServerCore.Main.Party;
using ServerCore.Main.Property;

namespace ServerCore.Main.Users
{
    public class UserData : ServerData
    {
        public readonly Property<string> PlayerId = new("id", string.Empty);
        public readonly Property<string> CurrentLocationId = new("location_id", string.Empty);
        public readonly Property<bool> IsTimeout = new("is_timeout", false);

        public readonly DataCollection<string, PartyInviteData> Invites = new("invites");

        //уже существующее пати
        public PartyData PartyData = new(string.Empty);

        public string WorldId;
        public bool WorldFirstConnection = true;
        public Peer Peer;
    
        public UserData() : base("user_data")
        {
            Properties.Add(PlayerId.Id, PlayerId);
            Properties.Add(CurrentLocationId.Id, CurrentLocationId);
            Properties.Add(IsTimeout.Id, IsTimeout);
            
            Dataset.Add(Invites.Id, Invites);
        }
    }
}