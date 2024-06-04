using System.Collections.Generic;

namespace ServerCore.Main.Utilities.Presenter
{
    public class PresentersDictionary<T> : IPresenter
    {
        private readonly Dictionary<T, IPresenter> _presenters = new();
        
        public void Init()
        {
            foreach (var presenter in _presenters.Values)
            {
                presenter.Init();
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters.Values)
            {
                presenter.Dispose();
            }
        }
        
        public void Clear() => _presenters.Clear();
        
        public void Add(T element, IPresenter presenter) => _presenters.Add(element, presenter);

        public void Remove(T element)
        {
            if (!_presenters.ContainsKey(element)) return;
            
            _presenters[element].Dispose();
            _presenters.Remove(element);
        }
    }
}