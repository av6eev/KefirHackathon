using Presenter;
using Save.Single.Collection;
using SimpleJson;
using UnityEngine;

namespace Save.Single
{
    public class SaveSinglePresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SaveSingleModelCollection _collection;
        private readonly SaveSingleModel _model;

        public SaveSinglePresenter(IGameModel gameModel, SaveSingleModelCollection collection, SaveSingleModel model)
        {
            _gameModel = gameModel;
            _collection = collection;
            _model = model;
        }

        public void Init()
        {
            var rawData = PlayerPrefs.GetString(_model.SaveModel.SaveId);

            Debug.Log($"{_model.SaveModel.SaveId}: {rawData}");
            var containsData = !string.IsNullOrEmpty(rawData);
            
            if (containsData)
            {
                var data = new JsonParser(rawData).ParseAsDictionary();

                _model.FillFromSave(data.GetNode(_model.SaveModel.SaveId));
            }
            
            _model.Save();
            
            _model.SaveModel.ChangeEvent.OnChanged += HandleChange;
        }

        public void Dispose()
        {
            _model.SaveModel.ChangeEvent.OnChanged -= HandleChange;
        }

        private void HandleChange()
        {
            _model.Save();
        }
    }
}