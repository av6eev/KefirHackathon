using Server.Services;
using Server.Services.Friends;
using ServerCore.Main;
using ServerCore.Main.Commands;

namespace Server.CommandExecutors.Variants;

public class LoginCommandExecutor : CommandExecutor<LoginCommand>
{
    public LoginCommandExecutor(LoginCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        var playerId = Command.PlayerId;
        var serverData = new CharacterServerData
        {
            PlayerId = { Value = playerId },
            PlayerNickname = { Value = Command.PlayerNickname }
        };
        
        var userModel = GameModel.UsersCollection.Add(Peer, playerId, Command.PlayerNickname);
        
        if (GameModel.WorldsCollection.Worlds.TryGetValue(userModel.WorldId, out var worldData))
        {
            worldData.CharacterDataCollection.Add(userModel.PlayerId, serverData);
            
            Console.WriteLine($"Connect user: {userModel.PlayerId} to world: {worldData.Guid}");
        }
        else
        {
            Console.WriteLine($"No world with id {userModel.WorldId} was found!");
        }
        
        GameModel.ServicesCollection.Get<FriendsServiceModel>(ServiceType.Friends).Add(userModel);
    }
}