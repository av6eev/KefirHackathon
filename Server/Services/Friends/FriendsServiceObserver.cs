using Server.Users;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Services.Friends;

public class FriendsServiceObserver : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly FriendsServiceModel _model;

    public FriendsServiceObserver(ServerGameModel gameModel, FriendsServiceModel model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        _model.AddEvent.OnChanged += HandleUserConnect;
        _model.RemoveEvent.OnChanged += HandleUserDisconnect;
    }

    public void Dispose()
    {
        _model.AddEvent.OnChanged -= HandleUserConnect;
        _model.RemoveEvent.OnChanged -= HandleUserDisconnect;
    }

    private void HandleUserConnect(UserModel userModel)
    {
        Logger.Instance.Log($"User: {userModel.PlayerNickname} attached to friend service");
        
        foreach (var friendNickname in userModel.FriendsCollection.GetModels())
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(friendNickname, out var friendModel)) continue;
            
            if (!friendModel.FriendsCollection.OnlineFriends.Contains(userModel.PlayerNickname))
            {
                userModel.FriendsCollection.OnlineFriends.Add(friendNickname);
                friendModel.FriendsCollection.OnlineFriends.Add(userModel.PlayerNickname);
            }

            if (!friendModel.UserData.FriendsData.OnlineFriends.Contains(userModel.PlayerNickname))
            {
                userModel.UserData.FriendsData.OnlineFriends.Add(friendNickname);
                friendModel.UserData.FriendsData.OnlineFriends.Add(userModel.PlayerNickname);
            }
        }
    }

    private void HandleUserDisconnect(UserModel userModel)
    {
        Logger.Instance.Log($"User: {userModel.PlayerNickname} detached from friend service");
        
        foreach (var friendNickname in userModel.FriendsCollection.GetModels())
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(friendNickname, out var friendModel)) continue;
            
            friendModel.FriendsCollection.OnlineFriends.Remove(userModel.PlayerNickname);
            friendModel.UserData.FriendsData.OnlineFriends.Remove(userModel.PlayerNickname);
        }
    }
}