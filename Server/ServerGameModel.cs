using Server.Party.Collection;
using Server.Party.Invite.Collection;
using Server.Save.Single.Collection;
using Server.Services.Collection;
using Server.Users.Collection;
using Server.World.Collection;
using ServerCore.Main.Specifications;

namespace Server;

public class ServerGameModel
{
    public IUsersCollection UsersCollection { get; init; }
    public WorldsCollection WorldsCollection { get; init; }
    public PartiesCollection PartiesCollection { get; init; }
    public IPartyInviteCollection PartyInviteCollection { get; init; }
    public IServerSpecifications Specifications { get; init; }
    public ISaveSingleModelCollection SaveSingleModelCollection { get; init; }
    public IServicesCollection ServicesCollection { get; init; }
}