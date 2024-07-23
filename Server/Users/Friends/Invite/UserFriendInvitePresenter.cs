using System.Timers;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;
using Timer = System.Timers.Timer;

namespace Server.Users.Friends.Invite;

public class UserFriendInvitePresenter : IPresenter
{
    private const float SecondsToWait = 10f;
    
    private readonly ServerGameModel _gameModel;
    private readonly UserFriendsCollection _collection;
    private readonly UserFriendInviteModel _model;

    private Timer _timer;

    public UserFriendInvitePresenter(ServerGameModel gameModel, UserFriendsCollection collection, UserFriendInviteModel model)
    {
        _gameModel = gameModel;
        _collection = collection;
        _model = model;
    }

    public void Init()
    {
        _model.OnDecided += HandleDecision;

        _timer = new Timer(SecondsToWait * 1000f);
        _timer.Elapsed += HandleTimerElapsed;
        _timer.AutoReset = false;
        _timer.Start();
        
        Logger.Instance.Log($"Friend invite: {_model.InviteId} enable timer!");
    }

    public void Dispose()
    {
        _timer.Stop();
        _model.OnDecided -= HandleDecision;
        
        Logger.Instance.Log($"Friend invite: {_model.InviteId} has been disposed!");
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _collection.RemoveInvite(_model.InviteId);
        Logger.Instance.Log($"Friend invite: {_model.InviteId} has been disposed by timer!");
    }

    private void HandleDecision(bool decision)
    {
        _timer.Stop();

        if (!_gameModel.UsersCollection.TryGetUser(_model.InviteFromUserId, out var fromUser)) return;
        if (!_gameModel.UsersCollection.TryGetUser(_model.InvitedUserId, out var invitedUser)) return;

        if (decision)
        {
            fromUser.FriendsCollection.AddFriend(invitedUser.PlayerNickname);
            invitedUser.FriendsCollection.AddFriend(fromUser.PlayerNickname);
            
            fromUser.UserData.FriendsData.Friends.Add(invitedUser.PlayerNickname);
            invitedUser.UserData.FriendsData.Friends.Add(fromUser.PlayerNickname);
            
            Logger.Instance.Log($"User: {invitedUser.PlayerNickname} accepted invite and now friends with user: {fromUser.PlayerNickname}!");
        }
        else
        {
            Logger.Instance.Log($"User: {invitedUser.PlayerNickname} declined friend invite from user: {fromUser.PlayerNickname}!");
        }
        
        invitedUser.FriendsCollection.RemoveInvite(_model.InviteId);
    }
}