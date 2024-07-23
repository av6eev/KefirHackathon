using ServerCore.Main;
using ServerCore.Main.Commands;

namespace Server.CommandExecutors;

public abstract class CommandExecutor<T> : ICommandExecutor where T : BaseCommand
{
    protected readonly T Command;
    protected readonly ServerGameModel GameModel;
    protected readonly Peer Peer;
    
    protected CommandExecutor(T command, ServerGameModel gameModel, ref Peer peer)
    {
        Command = command;
        GameModel = gameModel;
        Peer = peer;
    }
    
    public abstract void Execute();
}