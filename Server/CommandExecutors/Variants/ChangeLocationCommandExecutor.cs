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
        
        WorldData newWorld;

        if (Command.ToId == userData.CurrentLocationId.Value)
        {
            return;
        }
                    
        if (Command.ToId.Equals("test_connection"))
        {
            newWorld = GameModel.WorldsCollection.Worlds["hub"];
        }
        else
        {
            newWorld = new WorldData(Guid.NewGuid().ToString());
            GameModel.WorldsCollection.Worlds.Add(newWorld.Guid, newWorld);
        }

        GameModel.WorldsCollection.Worlds[userData.WorldId].CharacterDataCollection.Remove(userData.PlayerId.Value);
                    
        userData.WorldId = newWorld.Guid;
        userData.CurrentLocationId.Value = Command.ToId;
                    
        var characterServerData = new CharacterServerData
        {
            PlayerId = { Value = userData.PlayerId.Value }
        };
                    
        GameModel.WorldsCollection.Worlds[newWorld.Guid].CharacterDataCollection.Add(userData.PlayerId.Value, characterServerData);
    }
}