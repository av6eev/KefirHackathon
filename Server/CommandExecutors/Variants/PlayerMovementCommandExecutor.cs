using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Utilities;

namespace Server.CommandExecutors.Variants;

public class PlayerMovementCommandExecutor : CommandExecutor<PlayerMovementCommand>
{
    public PlayerMovementCommandExecutor(PlayerMovementCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Peer, out var data))
        {
            Console.WriteLine($"[MovementCommandExecutor]: no user with peer: {Peer}");
            return;
        }
        
        var world = GameModel.WorldsCollection.Worlds[data.WorldId];

        if (!world.CharacterDataCollection.Collection.TryGetValue(Command.PlayerId, out var characterServerData))
        {
            Console.WriteLine($"[MovementCommandExecutor]: no world with id: {data.WorldId}");
            return;
        }
            
        characterServerData.LatestServerPosition.Value = new Vector3(Command.X, Command.Y, Command.Z);
        characterServerData.Rotation.Value = Command.RotationY;
        characterServerData.Speed.Value = Command.Speed;
    }
}