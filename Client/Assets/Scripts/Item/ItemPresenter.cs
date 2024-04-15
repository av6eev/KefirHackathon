using Presenter;

namespace Item
{
    public class ItemPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly ItemModel _model;
        private readonly IItemView _view;

        public ItemPresenter(IGameModel gameModel, ItemModel model, IItemView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}