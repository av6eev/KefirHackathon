using Server.Users.Friends.Invite;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Users.Friends;

public class UserFriendsCollectionPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly UserFriendsCollection _collection;

    private readonly PresentersDictionary<UserFriendInviteModel> _friendInvitePresenters = new();
    
    public UserFriendsCollectionPresenter(ServerGameModel gameModel, UserFriendsCollection collection)
    {
        _gameModel = gameModel;
        _collection = collection;
    }
    
    public void Init()
    {
        _collection.ChangeEvent.OnChanged += HandleCollectionChange;

        _collection.Invites.AddEvent.OnChanged += HandleFriendInviteCreated;
        _collection.Invites.RemoveEvent.OnChanged += HandleFriendInviteRemoved;
    }

    public void Dispose()
    {
        _collection.ChangeEvent.OnChanged -= HandleCollectionChange;

        _collection.Invites.AddEvent.OnChanged -= HandleFriendInviteCreated;
        _collection.Invites.RemoveEvent.OnChanged -= HandleFriendInviteRemoved;
    }

    private void HandleCollectionChange()
    {
        _collection.NotifySaveEvent.Invoke();
    }

    private void HandleFriendInviteCreated(UserFriendInviteModel inviteModel)
    {
        var presenter = new UserFriendInvitePresenter(_gameModel, _collection, inviteModel);
        presenter.Init();
        
        _friendInvitePresenters.Add(inviteModel, presenter);
    }

    private void HandleFriendInviteRemoved(UserFriendInviteModel inviteModel)
    {
        _friendInvitePresenters.Remove(inviteModel);
    }
}