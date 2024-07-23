using Server.Utilities.ModelCollection;
using ServerCore.Main.Utilities.Event;

namespace Server.Save.Group.Element
{
    public class SaveElementModel<T> : ISaveElementModel where T : ISaveModel
    {
        public ReactiveEvent ChangeEvent { get; }
        public string Key { get; }
        private readonly IModelCollection<T> _model;

        public SaveElementModel(string key, IModelCollection<T> model)
        {
            _model = model;
            Key = key;
            ChangeEvent = model.ChangeEvent;
        }

        public List<IDictionary<string, object>> GetSaveData()
        {
            var resultList = new List<IDictionary<string, object>>();

            foreach (var element in _model.GetModels())    
            {
                resultList.Add(element.GetSaveData());
            }

            return resultList;
        }

        public IEnumerable<List<object>> GetSaveDataList()
        {
            foreach (var element in _model.GetModels())    
            {
               yield return element.GetSaveDataList();
            }
        }
    }
    
    public class SaveElementModel<TKey, TValue> : ISaveElementModel where TValue : ISaveModel
    {
        public string Key { get; }
        public ReactiveEvent ChangeEvent { get; }
        private readonly IModelCollection<TKey, TValue> _model;

        public SaveElementModel(string key, IModelCollection<TKey, TValue> model)
        {
            _model = model;
            Key = key;
            ChangeEvent = model.ChangeEvent;
        }

        public List<IDictionary<string, object>> GetSaveData()
        {
            var resultList = new List<IDictionary<string, object>>();

            foreach (var element in _model.GetModels())    
            {
                resultList.Add(element.GetSaveData());
            }

            return resultList;
        }
        
        public IEnumerable<List<object>> GetSaveDataList()
        {
            foreach (var element in _model.GetModels())    
            {
                yield return element.GetSaveDataList();
            }
        }
    }
}