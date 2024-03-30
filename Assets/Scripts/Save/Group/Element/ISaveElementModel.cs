using System.Collections.Generic;
using Reactive.Event;
using Utilities.Model;

namespace Save.Group.Element
{
    public interface ISaveElementModel : IModel
    {
        string Key { get; }
        ReactiveEvent ChangeEvent { get; }
        List<IDictionary<string, object>> GetSaveData();
    }
}