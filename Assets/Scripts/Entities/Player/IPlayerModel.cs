using Reactive.Field;
using Save.Single;
using Utilities.ModelCollection;

namespace Entities.Player
{
    public interface IPlayerModel : IEntityModel, INotifySaveModel
    {
        ModelCollection<EntityResourceType, EntityResource> Resources { get; }
        string BaseLocationId { get; }
        ReactiveField<bool> InDash { get; }
    }
}