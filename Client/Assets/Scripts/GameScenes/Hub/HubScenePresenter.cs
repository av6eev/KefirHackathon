using Entities;
using InteractiveObjects.Portal;
using SceneManagement;

namespace GameScenes.Hub
{
    public class HubScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly HubSceneView _view;

        public HubScenePresenter(GameModel gameModel, HubSceneView view) : base(gameModel, view)
        {
            _gameModel = gameModel;
            _view = view;
        }
        
        protected override void AfterInit()
        {
            _gameModel.SceneManagementModelsCollection.SetCurrentSceneId(SceneConst.HubId);
            
            _gameModel.PlayerModel.Resources.GetModel(EntityResourceType.Amnesia).Amount.Value = 10;
            _gameModel.PlayerModel.Resources.GetModel(EntityResourceType.Health).Amount.Value = _gameModel.PlayerModel.Specification.MaxHealth;
            
            if (_gameModel.Rerun)
            {
                _gameModel.PlayerDialogModel.Add("И я снова здесь...");
                _gameModel.PlayerDialogModel.Add("Так какова же моя цель...?");
                _gameModel.PlayerDialogModel.Add("И кем я был...");
            }
            
            Presenters.Add(new PortalPresenter(_gameModel, _view.PortalView));
        }

        protected override void AfterDispose()
        {
        }
    }
}