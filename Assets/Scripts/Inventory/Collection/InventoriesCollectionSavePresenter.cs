using Presenter;
using Save.Group;
using SimpleJson;
using UnityEngine;

namespace Inventory.Collection
{
    public class InventoriesCollectionSavePresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly InventoriesCollection _model;

        private IPresenter _savePresenter;
        
        public InventoriesCollectionSavePresenter(IGameModel gameModel, InventoriesCollection model)
        {
            _gameModel = gameModel;
            _model = model;
        }

        public void Init()
        {
            var saveGroup = new SaveGroupModel(InventoriesCollection.Id);
            var rawData = PlayerPrefs.GetString(InventoriesCollection.Id);

            Debug.Log("inventories: " + rawData);
            var containsData = !string.IsNullOrEmpty(rawData);
            
            if (containsData)
            {
                var inventoryData = new JsonParser(rawData).ParseAsDictionary();

                _model.FillFromSave(inventoryData.GetNodes(InventoriesCollection.Id));
            }
            
            saveGroup.Add(InventoriesCollection.Id, _model);
            
            _savePresenter = new SaveGroupPresenter(_gameModel, saveGroup);
            _savePresenter.Init();
        }

        public void Dispose()
        {
            _savePresenter.Dispose();
        }
    }
}