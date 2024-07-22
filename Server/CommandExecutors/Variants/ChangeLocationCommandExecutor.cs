using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.World;

namespace Server.CommandExecutors.Variants;

public class ChangeLocationCommandExecutor : CommandExecutor<ChangeLocationCommand>
{
    public ChangeLocationCommandExecutor(ChangeLocationCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.PlayerId, out var userModel)) return;
        if (Command.ToId == userModel.CurrentLocationId) return;

        WorldData newWorld;

        if (Command.ToId.Equals("test_connection"))
        {
            newWorld = GameModel.WorldsCollection.Worlds["hub"];
        }
        else
        {
            newWorld = new WorldData(Guid.NewGuid().ToString());
            GameModel.WorldsCollection.Worlds.Add(newWorld.Guid, newWorld); 
        }

        var characterServerData = new CharacterServerData
        {
            PlayerId = { Value = userModel.PlayerId }
        };
        
        if (!string.IsNullOrEmpty(userModel.CurrentLocationId))
        {
            GameModel.WorldsCollection.Worlds[userModel.WorldId].CharacterDataCollection.Remove(userModel.PlayerId);
            Console.WriteLine($"Remove user: {userModel.PlayerId} from world: {userModel.WorldId}");
        }

        if (!string.IsNullOrEmpty(userModel.CurrentLocationId))
        {
            GameModel.WorldsCollection.Worlds[newWorld.Guid].CharacterDataCollection.Add(userModel.PlayerId, characterServerData);
            Console.WriteLine($"Add user: {userModel.PlayerId} to world: {newWorld.Guid}");
        }
        
        userModel.ChangeWorld(newWorld.Guid);
        Console.WriteLine($"Change location for user {userModel.PlayerId} from: {userModel.CurrentLocationId} , to: {Command.ToId}, world: {newWorld.Guid}");
        userModel.ChangeLocation(Command.ToId);
    }
}