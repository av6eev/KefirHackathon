using Dialogs;
using Dialogs.Collection;
using Inventory.Slot;
using Presenter;

namespace Inventory.Dialog
{
    public abstract class InventoryDialogPresenter : DialogPresenter<InventoryDialogView>
    {
        private readonly IGameModel _gameModel;
        protected readonly InventoryDialogModel Model;
        private readonly DialogsCollectionView _collectionView;

        private readonly PresentersList _slotPresenters = new();
        
        public InventoryDialogPresenter(IGameModel gameModel, InventoryDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
        {
            _gameModel = gameModel;
            Model = model;
            _collectionView = collectionView;
        }

        protected override void AfterInit()
        {
            foreach (var slot in Model.InventoryModel.GetModels())
            {
                var view = View.Slots[slot.Index];
                view.Index = slot.Index;
                
                var presenter = new InventorySlotPresenter(_gameModel, slot, view);
                _slotPresenters.Add(presenter);
            }
            
            _slotPresenters.Init();
            
            AfterInitInventory();
        }

        protected override void AfterDispose()
        {
            AfterDisposeInventory();
            
            _slotPresenters.Dispose();
            _slotPresenters.Clear();
        }

        protected abstract void AfterInitInventory();
        protected abstract void AfterDisposeInventory();
    }
}