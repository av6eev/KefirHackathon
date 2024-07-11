using System.Collections.Generic;
using System.Threading.Tasks;
using Cameras;
using Entities.Animation;
using Entities.Player;
using Entities.Player.Dialog;
using LoadingScreen;
using Presenter;
using Quest.Collection;
using UnityEngine;

namespace GameScenes
{
    public abstract class BaseGameScenePresenter : IPresenter
    {
        protected readonly GameModel GameModel;
        private readonly BaseGameSceneView _view;

        protected readonly Dictionary<string, IPresenter> Presenters = new();
        
        protected BaseGameScenePresenter(GameModel gameModel, BaseGameSceneView view)
        {
            GameModel = gameModel;
            _view = view;
        }
        
        public async void Init()
        {
            Presenters.Add(LoadingScreenMessageConst.CameraPresenter, new CameraPresenter(GameModel, (CameraModel)GameModel.CameraModel, _view.CameraView));
            Presenters.Add(LoadingScreenMessageConst.PlayerPresenter, new PlayerPresenter(GameModel, (PlayerModel)GameModel.PlayerModel, null));
            // Presenters.Add(new PlayerDialogPresenter(GameModel, GameModel.PlayerDialogModel, _view.PlayerView.DialogView));
            // Presenters.Add(new EntityAnimationPresenter(_view.PlayerView.EntityAnimationEvents, GameModel.PlayerModel.AnimationEvents));
            
            AfterInit();
            
            GameModel.LoadingScreenModel.SetMaxLoadElementsCount(Presenters.Count);

            foreach (var presenter in Presenters)
            {
                GameModel.LoadingScreenModel.UpdateScreenMessage(presenter.Key);
                GameModel.LoadingScreenModel.IncrementProgressValue();

                presenter.Value.Init();
                
                await Task.Delay(1000);
            }

            await Task.Delay(1500);
            
            GameModel.LoadingScreenModel.Hide();
        }

        public void Dispose()
        {
            GameModel.LoadingScreenModel.Show();

            foreach (var presenter in Presenters.Values)
            {
                presenter.Dispose();
            }
            
            Presenters.Clear();
            
            AfterDispose();

            GameModel.EnemiesCollection.Clear();
            GameModel.QuestsCollection.Clear();
        }

        protected abstract void AfterInit();
        protected abstract void AfterDispose();
    }
}