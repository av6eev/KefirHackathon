using ServerCore.Main.Utilities.Event;

namespace Server.Save.Single
{
    public interface INotifySaveModel : ISaveModel
    {
        ReactiveEvent NotifySaveEvent { get; }
        string SaveId { get; }
        string SaveFileName { get; }
        void SetSaveData(IDictionary<string, object> nodes);
    }
}