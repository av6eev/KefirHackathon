using Server.Utilities.Model;

namespace Server.Save
{
    public interface ISaveModel : IModel
    {
        IDictionary<string, object> GetSaveData();
        List<object> GetSaveDataList();
    }
}