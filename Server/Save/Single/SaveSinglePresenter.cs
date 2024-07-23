using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Server.Save.Single.Collection;
using ServerCore.Main.Utilities;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Save.Single
{
    public class SaveSinglePresenter : IPresenter
    {
        private readonly ServerGameModel _gameModel;
        private readonly SaveSingleModelCollection _collection;
        private readonly SaveSingleModel _model;

        public SaveSinglePresenter(ServerGameModel gameModel, SaveSingleModelCollection collection, SaveSingleModel model)
        {
            _gameModel = gameModel;
            _collection = collection;
            _model = model;
        }

        public void Init()
        {
            var path = ServerConst.SavingFilesPath + _model.SaveModel.SaveFileName + ".csv";
            var separator = ";";
            var resultDictionary = new Dictionary<string, object>();    
            
            if (File.Exists(path))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = separator,
                    BadDataFound = null
                };
            
                using var streamReader = new StreamReader(path);
                using var csvReader = new CsvReader(streamReader, config);
                csvReader.Read();
                csvReader.ReadHeader();
            
                while (csvReader.Read())
                {
                    var idField = csvReader.GetField<string>(0);
                    
                    if (idField != _model.SaveModel.SaveId)
                    {
                        continue;
                    }
                    
                    for (var i = 0; i < csvReader.HeaderRecord.Length; i++)
                    {
                        resultDictionary.Add(csvReader.HeaderRecord[i], csvReader.GetField(i));
                    }
                    
                    // resultDictionary.Add("nickname", csvReader.GetField<string>(1));
                    // resultDictionary.Add("friends", JsonConvert.DeserializeObject<List<object>>(csvReader.GetField<string>(2)));
                }
            
                streamReader.Close();
            }

            if (resultDictionary.Count == 0)
            {
                Logger.Instance.Log($"No saved data for SaveId: {_model.SaveModel.SaveId}");
            }
            else
            {
                _model.FillFromSave(resultDictionary);
            }
            
            HandleNotify();

            _model.SaveModel.NotifySaveEvent.OnChanged += HandleNotify;
        }

        public void Dispose()
        {
            HandleNotify();
            
            _model.SaveModel.NotifySaveEvent.OnChanged -= HandleNotify;
        }

        private void HandleNotify()
        {
            _model.Save();
        }
    }
}