using DebugUI;
using Presenter;

namespace GameScenes.GameUI.DebugPanel
{
    public class DebugPanelPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly DebugPanelModel _model;
        private readonly DebugPanelView _view;

        public DebugPanelPresenter(GameModel gameModel, DebugPanelModel model, DebugPanelView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.gameObject.SetActive(false);
            
            _gameModel.InputModel.OnDebugPanelToggle += HandleDebugPanelToggle;
        }

        public void Dispose()
        {
            _gameModel.InputModel.OnDebugPanelToggle -= HandleDebugPanelToggle;
        }

        private void HandleDebugPanelToggle()
        {
            _model.IsOpen = !_model.IsOpen;

            switch (_model.IsOpen)
            {
                case true:
                    _view.gameObject.SetActive(true);
                    
                    ConfigureBuilder();
                    break;
                case false:
                    _view.gameObject.SetActive(false);
                    break;
            }
        }

        private void ConfigureBuilder()
        {
            var builder = new DebugUIBuilder();

            builder.ConfigureWindowOptions(options =>
            {
                options.Title = "Debug Panel";
                options.Draggable = true;
            });

            builder.AddField("speed", () => _gameModel.PlayerModel.CurrentSpeed.Value);
            builder.AddField("player_id", () => _gameModel.PlayerModel.UserData.PlayerId.Value);
            builder.AddField("location_id", () => _gameModel.PlayerModel.UserData.CurrentLocationId.Value);
            builder.AddField("in_party", () => _gameModel.PlayerModel.UserData.PartyData.InParty.Value);
            builder.AddField("party_members_count", () => _gameModel.PlayerModel.UserData.PartyData.Members.Collection.Count);
            builder.AddField("world_players_count", () => _gameModel.WorldData.CharacterDataCollection.Collection.Count);
            builder.BuildWith(_view.UIDocument);
        }
    }
}