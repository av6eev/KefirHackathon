using System.Collections.Generic;

namespace ServerCore.Main.Utilities.Presenter
{
    public class PresentersList : IPresenter
    {
        private readonly List<IPresenter> _presenters = new();

        public void Add(IPresenter presenter) => _presenters.Add(presenter);

        public void Init()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Init();
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }
        }

        public void Clear() => _presenters.Clear();

        public void Remove(IPresenter presenter)
        {
            if (!_presenters.Contains(presenter)) return;
            
            presenter.Dispose();
            _presenters.Remove(presenter);
        }
    }
}