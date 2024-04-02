using System.Collections.Generic;
using Loader.Scene;
using Presenter;
using UnityEngine;

namespace Utilities.Loader.Addressable.Scene
{
    public class AddressableSceneLoadWrapper : LoadSceneWrapper<GameModel>
    {
        private readonly PresentersDictionary<ILoadSceneModel> _presenters = new();
        private readonly Dictionary<ILoadSceneModel, AddressableSceneLoadWrapperModel> _wrapperModels = new();

        public AddressableSceneLoadWrapper(GameModel gameModel) : base(gameModel)
        {
        }

        public override void Load(LoadSceneModel model)
        {
            var wrapperModel = new AddressableSceneLoadWrapperModel(model);
            
            _wrapperModels.Add(model, wrapperModel);
            var presenter = new AddressableSceneLoadWrapperPresenter(GameModel, wrapperModel);
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        public override async void Unload(ILoadSceneModel model)
        {
            var presenter = new AddressableSceneUnloadWrapperPresenter(_wrapperModels[model]);
            presenter.Init();
            
            _presenters.Remove(model);

            await model.UnloadAwaiter;
            
            presenter.Dispose();
        }
    }
}