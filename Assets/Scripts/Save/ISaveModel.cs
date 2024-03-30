using System.Collections.Generic;
using Utilities.Model;

namespace Save
{
    public interface ISaveModel : IModel
    {
        IDictionary<string, object> GetSaveData();
    }
}