using Server.Services.Friends;
using Server.Utilities.ModelCollection;

namespace Server.Services.Collection;

public interface IServicesCollection : IModelCollection<ServiceType, IServiceModel>
{
    void Register(IServiceModel friendsServiceModel);
    T Get<T>(ServiceType type) where T : IServiceModel;
}