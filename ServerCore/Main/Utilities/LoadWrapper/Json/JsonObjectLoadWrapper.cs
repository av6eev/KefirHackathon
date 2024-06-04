using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.Presenter;

namespace ServerCore.Main.Utilities.LoadWrapper.Json
{
    public class JsonObjectLoadWrapper : ILoadObjectWrapper
    {
        private readonly PresentersDictionary<IWrapperLoadModel> _presenters = new();

        public void Load(ILoadObjectToWrapperModel model)
        {
            var element = new JsonObjectLoadWrapperModel(model);
            var presenter = new JsonObjectLoadWrapperPresenter(element);
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        public void Unload(IWrapperLoadModel model)
        {
            _presenters.Remove(model);
        }
    }
}