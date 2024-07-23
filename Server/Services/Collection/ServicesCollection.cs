using Server.Utilities.ModelCollection;

namespace Server.Services.Collection;

public class ServicesCollection : ModelCollection<ServiceType, IServiceModel>, IServicesCollection
{
    public void Register(IServiceModel serviceModel)
    {
        Add(serviceModel.Type, serviceModel);
    }

    public T Get<T>(ServiceType type) where T : IServiceModel
    {
        var model = GetModel(type);
        return (T)model;
    }
}