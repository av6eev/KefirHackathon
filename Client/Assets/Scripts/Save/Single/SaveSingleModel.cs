using System.Collections.Generic;
using UnityEngine;
using Utilities.Model;
using Newtonsoft.Json;

namespace Save.Single
{
    public class SaveSingleModel : IModel
    {
        public INotifySaveModel SaveModel { get; }

        public SaveSingleModel(INotifySaveModel saveModel)
        {
            SaveModel = saveModel;
        }

        public void Save()
        {
            var resultDictionary = new Dictionary<string, object>
            {
                { SaveModel.SaveId, SaveModel.GetSaveData() }
            };

            var resultToJsonString = JsonConvert.SerializeObject(resultDictionary);
            PlayerPrefs.SetString(SaveModel.SaveId, resultToJsonString);
        }

        public void FillFromSave(IDictionary<string, object> nodes)
        {
            SaveModel.SetSaveData(nodes);
        }
    }
}