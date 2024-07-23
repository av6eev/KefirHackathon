using Server.Utilities.Model;
using ServerCore.Main.Utilities.Event;

namespace Server.Save.Group.Element
{
    public interface ISaveElementModel : IModel
    {
        ReactiveEvent ChangeEvent { get; }
        string Key { get; }
        List<IDictionary<string, object>> GetSaveData();
        IEnumerable<List<object>> GetSaveDataList();
    }
}