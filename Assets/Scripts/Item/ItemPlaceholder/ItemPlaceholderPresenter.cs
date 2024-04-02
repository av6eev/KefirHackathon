using Presenter;
using UnityEngine;

namespace Item.ItemPlaceholder
{
    public class ItemPlaceholderPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly ItemPlaceholderModel _model;
        private readonly ItemPlaceholderView _view;

        public ItemPlaceholderPresenter(IGameModel gameModel, ItemPlaceholderModel model, ItemPlaceholderView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _model.Position.OnChanged += ChangePosition;
            _model.ShowEvent.OnChanged += HandleShow;
            _model.HideEvent.OnChanged += HandleHide;
            _view.Drop += HandleDrop;
        }

        public void Dispose()
        {
            _model.Position.OnChanged -= ChangePosition;
            _model.ShowEvent.OnChanged -= HandleShow;
            _model.HideEvent.OnChanged -= HandleHide;
            _view.Drop -= HandleDrop;
        }

        private void HandleDrop(string inventoryId, int index)
        {
            var fromInventory = _gameModel.InventoriesCollection.GetModel(_model.FromInventoryId);
            var toInventory = _gameModel.InventoriesCollection.GetModel(inventoryId);

            var toInventorySlot = toInventory.GetSlot(index);
            var fromInventorySlot = fromInventory.GetSlot(_model.FromIndex);

            var fromSlotSpecification = _gameModel.Specifications.ItemSpecifications[fromInventorySlot.ItemId];
            
            if (toInventorySlot.IsNeedType && !toInventorySlot.ItemType.Equals(fromSlotSpecification.ItemType))
            {
                return;
            }
            
            if (!toInventorySlot.IsEmpty)
            {
                var itemId = toInventorySlot.ItemId;
                var itemAmount = toInventorySlot.ItemAmount;

                toInventorySlot.ItemId = fromInventorySlot.ItemId;
                toInventorySlot.ItemAmount = fromInventorySlot.ItemAmount;

                fromInventorySlot.ItemId = itemId;
                fromInventorySlot.ItemAmount = itemAmount;
            }
            else
            {
                toInventory.AddItem(index, fromInventorySlot.ItemId, fromInventorySlot.ItemAmount);
                
                fromInventory.RemoveItem(_model.FromIndex, fromInventorySlot.ItemAmount);
            }
        }

        private void ChangePosition(Vector2 newPosition, Vector2 oldPosition)
        {
            var rect = _view.Root.rect;
            var height = rect.height / 4;
            var width = rect.width / 4;
            var offset = new Vector3(-width, height, 0);
            
            _view.Root.transform.position = new Vector3(newPosition.x, newPosition.y, 0) + offset;
        }

        private void HandleShow()
        {
           _view.Root.gameObject.SetActive(true);

           var inventory = _gameModel.InventoriesCollection.GetModel(_model.FromInventoryId);
           var slot = inventory.GetModel(_model.FromIndex);
           var itemSpecification = _gameModel.Specifications.ItemSpecifications[slot.ItemId];
           
           _view.IconImage.sprite = itemSpecification.IconSprite;
        }

        private void HandleHide()
        {
            _view.Root.gameObject.SetActive(false); 
        }
    }
}