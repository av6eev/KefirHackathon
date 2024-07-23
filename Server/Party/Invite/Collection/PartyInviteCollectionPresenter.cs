using ServerCore.Main.Utilities.Presenter;

namespace Server.Party.Invite.Collection;

public class PartyInviteCollectionPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly PartyInviteCollection _model;

    private readonly PresentersDictionary<PartyInviteModel> _partyInvitePresenters = new();

    public PartyInviteCollectionPresenter(ServerGameModel gameModel, PartyInviteCollection model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        _model.OnInviteCreated += HandleInviteCreate;
        _model.OnInviteRemoved += HandleInviteRemove;
    }

    public void Dispose()
    {
        _model.OnInviteCreated -= HandleInviteCreate;
        _model.OnInviteRemoved -= HandleInviteRemove;
    }

    private void HandleInviteCreate(PartyInviteModel inviteModel)
    {
        var presenter = new PartyInvitePresenter(_gameModel, inviteModel);
        presenter.Init();
        
        _partyInvitePresenters.Add(inviteModel, presenter);
    }

    private void HandleInviteRemove(PartyInviteModel inviteModel)
    {
        _partyInvitePresenters.Remove(inviteModel);
    }
}