using System.Collections.Generic;
using Inventory.Slot;
using Item;
using Presenter;

namespace PlayerInventory.Hud
{
    public class PlayerInventoryHudPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerInventoryHudModel _model;
        private readonly PlayerInventoryHudView _view;
        
        private readonly PresentersList _slotPresenters = new();
        
        private readonly List<ItemType> _itemTypes = new()
        {
            ItemType.Weapon, ItemType.Potion, ItemType.Potion, ItemType.Potion,
        };
        
        public PlayerInventoryHudPresenter(IGameModel gameModel, PlayerInventoryHudModel model, PlayerInventoryHudView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            for (var i = 0; i < _itemTypes.Count; i++)
            {
                _view.Slots[i].NeedType = true;
                _view.Slots[i].ItemType = _itemTypes[i];

                var slotModel = _model.InventoryModel.GetModel(i);
                
                slotModel.IsNeedType = true;
                slotModel.ItemType = _itemTypes[i];
                
                var presenter = new InventorySlotPresenter(_gameModel, slotModel, _view.Slots[i]);
                _slotPresenters.Add(presenter);
            }
            
            _slotPresenters.Init();

            _gameModel.InputModel.OnSlotStateChanged += HandleSlotStateChanged;
        }

        public void Dispose()
        {
            _slotPresenters.Dispose();    
            _slotPresenters.Clear();
            
            _gameModel.InputModel.OnSlotStateChanged -= HandleSlotStateChanged;
        }

        private void HandleSlotStateChanged(int index, bool state)
        {
            var slot = _model.InventoryModel.GetModel(index);
            var currentActiveSlot = _model.InventoryModel.GetActiveSlot();
            
            if (currentActiveSlot != null)
            {
                currentActiveSlot.IsActive.Value = false;
            }

            if (currentActiveSlot != slot)
            {
                slot.IsActive.Value = state;
            }
        }
    }
}