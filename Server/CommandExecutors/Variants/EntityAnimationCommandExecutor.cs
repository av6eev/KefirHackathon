using ServerCore.Main;
using ServerCore.Main.Commands;

namespace Server.CommandExecutors.Variants;

public class EntityAnimationCommandExecutor : CommandExecutor<EntityAnimationCommand>
{
    public EntityAnimationCommandExecutor(EntityAnimationCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Peer, out var player)) return;
        
        var worldData = GameModel.WorldsCollection.Worlds[player.WorldId];

        if (!worldData.CharacterDataCollection.Collection.TryGetValue(Command.PlayerId, out var entityServerData)) return;
            
        entityServerData.AnimationState.Value = Command.AnimationState;
    }
}