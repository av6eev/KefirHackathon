using System.Collections.Generic;
using Reactive.Event;

namespace Save.Single
{
    public interface INotifySaveModel : ISaveModel
    {
        string SaveId { get; }
        ReactiveEvent ChangeEvent { get; }
        void SetSaveData(IDictionary<string, object> nodes);
    }
}