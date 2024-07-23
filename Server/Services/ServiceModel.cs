using Server.Utilities.Model;
using Server.Utilities.ModelCollection;

namespace Server.Services;

public abstract class ServiceModel : IServiceModel
{
    public ServiceType Type { get; }

    protected ServiceModel(ServiceType type)
    {
        Type = type;
    }
}

public abstract class ServiceModel<TValue> : ModelCollection<TValue>, IServiceModel<TValue>
{
    public ServiceType Type { get; }

    protected ServiceModel(ServiceType type)
    {
        Type = type;
    }
}

public abstract class ServiceModel<TKey, TModel> : ModelCollection<TKey, TModel>, IServiceModel<TKey, TModel> where TModel : IModel
{
    public ServiceType Type { get; }

    protected ServiceModel(ServiceType type)
    {
        Type = type;
    }
}