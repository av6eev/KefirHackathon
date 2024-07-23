using Newtonsoft.Json;
using Server.Save.Single;
using Server.Users.Friends;
using ServerCore.Main;
using ServerCore.Main.Users;
using ServerCore.Main.Utilities.Event;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.SimpleJson;

namespace Server.Users;

public class UserModel : INotifySaveModel
{
    public ReactiveEvent NotifySaveEvent { get; } = new();
    public string SaveId { get; }
    public string SaveFileName => "users";

    public string PlayerId { get; private set; }
    public string PlayerNickname { get; private set; }
    public string WorldId;
    public string CurrentLocationId;
    
    public bool IsTimeout = false;
    public bool IsOnline = true;
    public bool WorldFirstConnection;
    
    public readonly Peer Peer;
    public readonly UserData UserData;

    public readonly UserFriendsCollection FriendsCollection;
    
    public UserModel(Peer peer, string id, string nickname)
    {
        Peer = peer;
        PlayerId = id;
        PlayerNickname = nickname;
        
        WorldFirstConnection = true;
        WorldId = "hub";
        CurrentLocationId = "test_connection";
        
        SaveId = id;
        FriendsCollection = new UserFriendsCollection(NotifySaveEvent);
        
        UserData = new UserData
        {
            PlayerId = { Value = PlayerId },
            PlayerNickname = { Value = PlayerNickname },
            CurrentLocationId = { Value = "test_connection" },
        };
    }
    
    public void ChangeWorld(string worldId, bool isWorldFirstConnection = true)
    {
        WorldId = worldId;
        WorldFirstConnection = isWorldFirstConnection;
    }
    
    public void ChangeLocation(string locationId)
    {
        CurrentLocationId = locationId;
        UserData.CurrentLocationId.Value = locationId;
    }

    public IDictionary<string, object> GetSaveData()
    {
        var resultData = new Dictionary<string, object>
        {
            { "id", PlayerId },
            { "nickname", PlayerNickname }
        };

        return resultData;
    }
    
    public List<object> GetSaveDataList()
    {
        var resultData = new List<object>
        {
            new
            {
                id = PlayerId,
                nickname = PlayerNickname,
                is_online = IsOnline,
                friends = FriendsCollection.GetModels()
            }
        };

        return resultData;
    }

    public void SetSaveData(IDictionary<string, object> nodes)
    {
        PlayerId = nodes.GetString("id");
        PlayerNickname = nodes.GetString("nickname");

        var friendsData = JsonConvert.DeserializeObject<List<object>>(nodes.GetString("friends"));
        
        foreach (var friend in friendsData)
        {
            FriendsCollection.Add(friend.ToString());
            UserData.FriendsData.Friends.Add(friend.ToString());
        }
        
        Logger.Instance.Log($"Set saved data for user: {PlayerNickname}");
    }
}