using Server.Utilities.Model;
using Server.Utilities.ModelCollection;

namespace Server.Services;

public interface IServiceModel : IModel
{
    ServiceType Type { get; }
}

public interface IServiceModel<TKey> : IModelCollection<TKey>, IServiceModel
{
}

public interface IServiceModel<in TKey, TModel> : IModelCollection<TKey, TModel>, IServiceModel
{
}