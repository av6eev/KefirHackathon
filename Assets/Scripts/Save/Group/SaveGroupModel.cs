using System.Collections.Generic;
using Save.Group.Element;
using Newtonsoft.Json;
using UnityEngine;
using Utilities.ModelCollection;

namespace Save.Group
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
            var resultDictionary = new Dictionary<string, object>();
            
            foreach (var saveModel in Collection)
            {
                resultDictionary.Add(saveModel.Key, saveModel.GetSaveData());
            }

            var resultToJsonString = JsonConvert.SerializeObject(resultDictionary);    
            PlayerPrefs.SetString(_key, resultToJsonString);
        }
    }
}