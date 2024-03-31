using GameScenes.GameUI;
using Presenter;
using SceneManagement;

namespace Entities.Player
{
    public class PlayerAmnesiaPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly IPlayerModel _model;
        private readonly PlayerMainResourceView _view;

        public PlayerAmnesiaPresenter(IGameModel gameModel, IPlayerModel model, PlayerMainResourceView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            var amnesiaResource = _model.Resources.GetModel(EntityResourceType.Amnesia);
            
            _view.FillBar.fillAmount = CalculateAmnesia(amnesiaResource.Amount.Value);
            _view.PercentageText.text = $"{amnesiaResource.Amount.Value}%";

            amnesiaResource.Amount.OnChanged += HandleAmnesiaChanged;
        }

        public void Dispose()
        {
            _model.Resources.GetModel(EntityResourceType.Amnesia).Amount.OnChanged -= HandleAmnesiaChanged;
        }

        private void HandleAmnesiaChanged(float newAmnesia, float oldAmnesia)
        {
            if (_gameModel.SceneManagementModelsCollection.CurrentSceneId == SceneConst.HubId) return;
            
            _view.FillBar.fillAmount = CalculateAmnesia(newAmnesia);
            _view.PercentageText.text = $"{newAmnesia}%";
        }

        private float CalculateAmnesia(float newAmnesia)
        {
            return newAmnesia / 100f;
        }
    }
}