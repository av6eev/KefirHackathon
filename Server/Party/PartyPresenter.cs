using Server.Party.Observers;
using ServerCore.Main.Party;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Party;

public class PartyPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly PartyModel _model;

    private readonly PresentersDictionary<string> _inviteObservers = new();
    
    public PartyPresenter(ServerGameModel gameModel, PartyModel model)
    {
        _gameModel = gameModel;
        _model = model;
    }

    public void Init()
    {
        _model.OnInviteCreated += HandleCreateInvite;
        _model.OnInviteRemoved += HandleRemoveInvite;
    }

    public void Dispose()
    {
        _model.OnInviteCreated -= HandleCreateInvite;
        _model.OnInviteRemoved -= HandleRemoveInvite;
    }

    private void HandleRemoveInvite(string invitedUserId)
    {
        _inviteObservers.Remove(invitedUserId);
    }

    private void HandleCreateInvite(PartyInviteData inviteData)
    {
        var observer = new PartyInviteDecisionObserver(_gameModel, inviteData);
        observer.Init();
        
        _inviteObservers.Add(inviteData.InvitedUserId.Value, observer);
    }
}