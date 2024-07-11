using Server.Party.Collection;
using Server.Party.Invite.Collection;
using Server.World.Collection;
using ServerCore.Main.Specifications;
using ServerCore.Main.Users.Collection;

namespace Server;

public class ServerGameModel
{
    public IUsersCollection UsersCollection { get; set; }
    public WorldsCollection WorldsCollection { get; set; }
    public PartiesCollection PartiesCollection { get; set; }
    public IPartyInviteCollection PartyInviteCollection { get; set; }
    public IServerSpecifications Specifications { get; set; }
}