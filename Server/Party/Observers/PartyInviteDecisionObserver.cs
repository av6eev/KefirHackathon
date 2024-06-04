using System.Timers;
using ServerCore.Main.Party;
using ServerCore.Main.Utilities.Presenter;
using Timer = System.Timers.Timer;

namespace Server.Party.Observers;

public class PartyInviteDecisionObserver : IPresenter
{
    private const float SecondsToWait = 10f;

    private readonly ServerGameModel _gameModel;
    private readonly PartyInviteData _inviteData;
    
    private Timer _timer;

    public PartyInviteDecisionObserver(ServerGameModel gameModel, PartyInviteData inviteData)
    {
        _gameModel = gameModel;
        _inviteData = inviteData;
    }

    public void Init()
    {
        _inviteData.OnDecided += HandleDecision;

        _timer = new Timer(SecondsToWait);
        _timer.Elapsed += HandleTimerElapsed;
        _timer.AutoReset = false;
        _timer.Start();
    }

    public void Dispose()
    {
        _inviteData.OnDecided -= HandleDecision;
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (!_inviteData.IsDecide)
        {
            _gameModel.PartiesCollection.Remove(_inviteData.InvitedPartyId.Value);
        }
    }

    private void HandleDecision()
    {
        _timer.Stop();

        if (!_inviteData.IsDecide) return;
        
        if (_inviteData.Result)
        {
            
        }
        else
        {
            
        }
    }
}