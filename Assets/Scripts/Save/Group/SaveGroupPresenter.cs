using Presenter;
using Save.Group.Element;

namespace Save.Group
{
    public class SaveGroupPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SaveGroupModel _model;

        private readonly PresentersDictionary<ISaveElementModel> _presenters = new();
        
        public SaveGroupPresenter(IGameModel gameModel, SaveGroupModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            foreach (var model in _model.GetModels())    
            {
                var presenter = new SaveElementPresenter(_gameModel, _model, model);
                presenter.Init();
                _presenters.Add(model, presenter);
            }
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
        }
    }
}