using Server.CommandExecutors.Variants;
using Server.CommandExecutors.Variants.Party;
using Server.Party.Collection;
using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Commands.Party;
using ServerCore.Main.Utilities;

namespace Server.CommandExecutors;

public class ExecutorFactory
{
    public ICommandExecutor? CreateExecutor(ServerGameModel gameModel, ref Event netEvent)
    {
        var readBuffer = new byte[1024];

        netEvent.Packet.CopyTo(readBuffer);

        var protocol = new Protocol(readBuffer);
        protocol.Get(out string commandName);

        return commandName switch
        {
            CommandConst.PlayerMovement => new PlayerMovementCommandExecutor(new PlayerMovementCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.Login => new LoginCommandExecutor(new LoginCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.EntityAnimation => new EntityAnimationCommandExecutor(new EntityAnimationCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.ChangeLocation => new ChangeLocationCommandExecutor(new ChangeLocationCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.InviteParty => new InvitePartyCommandExecutor(new InvitePartyCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.AcceptParty => new AcceptPartyCommandExecutor(new AcceptPartyCommand(protocol), gameModel, netEvent.Peer),
            CommandConst.DeclineParty => new DeclinePartyCommandExecutor(new DeclinePartyCommand(protocol), gameModel, netEvent.Peer),
            _ => null
        };
        
    }
}