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
        ReactiveField<bool> IsAfk { get; }
        ReactiveField<float> AfkTime { get; }
        ReactiveField<int> KillCount { get; }
        bool IsInputInverse { get; }
        void Death();
        void InverseInput(bool state);
    }
}