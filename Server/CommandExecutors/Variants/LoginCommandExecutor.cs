using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Users;

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
        var user = new UserData
        {
            Peer = Peer,
            PlayerId = { Value = playerId },
            PlayerNickname = { Value = Command.PlayerNickname },
            WorldId = "hub",
            CurrentLocationId = { Value = "test_connection" },
            WorldFirstConnection = true
        };

        if (GameModel.WorldsCollection.Worlds.TryGetValue(user.WorldId, out var worldData))
        {
            worldData.CharacterDataCollection.Add(user.PlayerId.Value, serverData);
            GameModel.UsersCollection.Add(user);
            
            Console.WriteLine($"Connect user: {user.PlayerId.Value} to world: {worldData.Guid}");
        }
        else
        {
            Console.WriteLine($"No world with id {user.WorldId} was found!");
        }
    }
}