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
        if (!GameModel.UsersCollection.TryGetUser(Peer, out var userData)) return;

        if (Command.ToId == userData.CurrentLocationId.Value) return;

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
            PlayerId = { Value = userData.PlayerId.Value }
        };
        
        if (!string.IsNullOrEmpty(userData.CurrentLocationId.Value))
        {
            GameModel.WorldsCollection.Worlds[userData.WorldId].CharacterDataCollection.Remove(userData.PlayerId.Value);
            Console.WriteLine($"Remove user: {userData.PlayerId.Value} from world: {userData.WorldId}");
        }

        if (!string.IsNullOrEmpty(userData.CurrentLocationId.Value))
        {
            GameModel.WorldsCollection.Worlds[newWorld.Guid].CharacterDataCollection.Add(userData.PlayerId.Value, characterServerData);
            Console.WriteLine($"Add user: {userData.PlayerId.Value} to world: {newWorld.Guid}");
        }
        
        userData.WorldId = newWorld.Guid;
        Console.WriteLine($"Change location for user {userData.PlayerId.Value} from: {userData.CurrentLocationId.Value} , to: {Command.ToId}, world: {newWorld.Guid}");
        userData.CurrentLocationId.Value = Command.ToId;
        userData.WorldFirstConnection = true;
    }
}