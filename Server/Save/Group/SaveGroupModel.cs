using System.Globalization;
using System.Text;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using Server.Save.Group.Element;
using Server.Users;
using Server.Utilities.ModelCollection;
using ServerCore.Main.Utilities;

namespace Server.Save.Group
{
    public class SaveGroupModel : ModelCollection<ISaveElementModel>
    {
        private readonly string _key;
        
        public SaveGroupModel(string key)
        {
            _key = key;
        }

        public void Add<T>(string key, IModelCollection<T> model) where T : ISaveModel
        {
           Add(new SaveElementModel<T>(key, model));
        }
        
        public void Add<TKey, TValue>(string key, IModelCollection<TKey, TValue> model) where TValue : ISaveModel
        {
            Add(new SaveElementModel<TKey, TValue>(key, model));
        }

        public void Save()
        {
            var path = ServerConst.SavingFilesPath;
            var resultList = new List<object>();

            foreach (var saveElement in Collection)
            {
                foreach (var saveData in saveElement.GetSaveDataList())
                {
                    resultList.AddRange(saveData);
                }
            }

            SaveHelper.SaveToFile(path, _key, resultList, new UTF8Encoding());
        }
    }
}