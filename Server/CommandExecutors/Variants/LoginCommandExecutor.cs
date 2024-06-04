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
            PlayerId = { Value = playerId }
        };
        var user = new UserData
        {
            Peer = Peer,
            PlayerId = { Value = playerId },
            WorldId = "hub",
            CurrentLocationId = { Value = "test_connection" }
        };
                
        GameModel.WorldsCollection.Worlds[user.WorldId].CharacterDataCollection.Add(user.PlayerId.Value, serverData);
        GameModel.UsersCollection.Add(user);
    }
}