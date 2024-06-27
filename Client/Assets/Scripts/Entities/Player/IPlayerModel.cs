using Reactive.Field;
using Save.Single;
using ServerCore.Main.Users;
using Utilities.ModelCollection;

namespace Entities.Player
{
    public interface IPlayerModel : IEntityModel, INotifySaveModel
    {
        string Id { get; set; }
        string BaseLocationId { get; }
        ReactiveField<bool> InDash { get; }
        ReactiveField<bool> IsAfk { get; }
        ReactiveField<float> AfkTime { get; }
        ReactiveField<int> KillCount { get; }
        ModelCollection<EntityResourceType, EntityResource> Resources { get; }
        UserData UserData { get; }
        bool IsInputInverse { get; }
        void Death();
        void InverseInput(bool state);
    }
}