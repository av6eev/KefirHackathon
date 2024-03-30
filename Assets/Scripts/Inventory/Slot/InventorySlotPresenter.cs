using Item;
using Presenter;
using UnityEngine;

namespace Inventory.Slot
{
    public class InventorySlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly InventorySlotModel _model;
        private readonly InventorySlotView _view;

        public InventorySlotPresenter(IGameModel gameModel, InventorySlotModel model, InventorySlotView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _model.IsActive.OnChanged += HandleStateChanged;
            _model.OnItemIdChanged += ChangeItemNameView;
            _model.OnItemAmountChanged += ChangeItemAmountView;

            _view.EndDrag += EndDrag;
            _view.Drag += Drag;
            _view.BeginDrag += BeginDrag;
            
            if (!_model.IsEmpty)
            {
                var itemSpecification = _gameModel.Specifications.ItemSpecifications[_model.ItemId];
                
                FillSlotView(itemSpecification.Name, true, _model.ItemAmount, itemSpecification.IconSprite);

                var inventoryModelSlots = _gameModel.InventoriesCollection.GetModel(_model.InventoryId);
                
                if (itemSpecification.ItemType == ItemType.Weapon && inventoryModelSlots.GetActiveSlot() == null)
                {
                    _model.IsActive.Value = true;
                }
            }
            else
            {
                FillSlotView(string.Empty, false);
            }

            _view.Index = _model.Index;
            _view.InventoryId = _model.InventoryId;
        }

        public void Dispose()
        {
            _model.OnItemIdChanged -= ChangeItemNameView;
            _model.OnItemAmountChanged -= ChangeItemAmountView;
            _model.IsActive.OnChanged -= HandleStateChanged;
            
            _view.EndDrag -= EndDrag;
            _view.Drag -= Drag;
            _view.BeginDrag -= BeginDrag;
        }
        
        private void Drag(Vector2 position)
        {
            if (_model.IsEmpty) return;
            
            _gameModel.ItemPlaceholderModel.SetPosition(position);
        }

        private void BeginDrag(Vector2 position)
        {
            if (_model.IsEmpty) return;
            
            _gameModel.ItemPlaceholderModel.Show(position, _model.InventoryId, _model.Index);
        }

        private void EndDrag()
        {
            // if (_model.IsEmpty) return;

            _gameModel.ItemPlaceholderModel.Hide();
        }

        private void ChangeItemAmountView(int amount)
        {
            if (_view.AmountText != null)
            {
                _view.AmountText.text = amount.ToString();
            }
        }
        
        private void HandleStateChanged(bool newState, bool oldState)
        {
            if (newState)
            {
                _view.HighlightBorderImage.gameObject.SetActive(true);
                _view.DefaultBorderImage.gameObject.SetActive(false);
            }
            else
            {
                _view.HighlightBorderImage.gameObject.SetActive(false);
                _view.DefaultBorderImage.gameObject.SetActive(true);
            }
        }

        private void ChangeItemNameView(string id)
        {
            if (!_model.IsEmpty)
            {
                var itemSpecification = _gameModel.Specifications.ItemSpecifications[id];
                Debug.Log(itemSpecification);
                FillSlotView(itemSpecification.Name, true, _model.ItemAmount, itemSpecification.IconSprite);
            }
            else
            {
                FillSlotView(string.Empty, false);
            }
        }

        private void FillSlotView(string itemName, bool enableIcon, int itemAmount = 0, Sprite itemIcon = null)
        {
            if (_view.NameText != null)
            {
                _view.NameText.text = itemName;
            }

            if (_view.AmountText != null)
            {
                if (itemAmount != 0)
                {
                    _view.AmountText.text = itemAmount.ToString();
                }
                else
                {
                    _view.AmountText.text = string.Empty;
                }
            }

            if (_view.IconImage != null)
            {
                if (enableIcon)
                {
                    _view.IconImage.sprite = itemIcon;
                    _view.IconImage.gameObject.SetActive(true);    
                }
                else
                {
                    _view.IconImage.gameObject.SetActive(false);
                }
            }
        }
    }
}