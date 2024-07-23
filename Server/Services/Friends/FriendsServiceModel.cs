using Server.Users;

namespace Server.Services.Friends;

public class FriendsServiceModel : ServiceModel<UserModel>
{
    public FriendsServiceModel() : base(ServiceType.Friends)
    {
    }
}