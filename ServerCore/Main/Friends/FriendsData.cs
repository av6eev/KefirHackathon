using ServerCore.Main.Property;

namespace ServerCore.Main.Friends
{
    public class FriendsData : ServerData
    {
        public readonly DataList<string> Friends = new("friends");
    
        public FriendsData(string id) : base(id)
        {
            Dataset.Add(Friends.Id, Friends);
        }
    }
}