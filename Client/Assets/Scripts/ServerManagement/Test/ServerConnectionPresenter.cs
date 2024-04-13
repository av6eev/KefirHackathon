using Presenter;
using ServerCore.Main;
using ServerManagement.Test.Player;
using ServerManagement.Test.World;
using Updater;

namespace ServerManagement.Test
{
    public class ServerConnectionPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly ServerConnectionModel _model;
        private IUpdater _playerConnectionUpdater;
        private IUpdater _worldConnectionUpdater;
        
        public ServerConnectionPresenter(GameModel gameModel, ServerConnectionModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _model.OnPlayerConnect += HandlePlayerConnect;
            _model.OnPlayerDisconnect += HandlePlayerDisconnect;
            _model.OnWorldConnect += HandleWorldConnect;
            _model.OnWorldDisconnect += HandleWorldDisconnect;
        }
        
        public void Dispose()
        {
            _model.OnPlayerConnect -= HandlePlayerConnect;
            _model.OnPlayerDisconnect -= HandlePlayerDisconnect;
            _model.OnWorldConnect -= HandleWorldConnect;
            _model.OnWorldDisconnect -= HandleWorldDisconnect;
        }

        private void HandlePlayerConnect()
        {
            Library.Initialize();

            var address = new Address();
            address.SetHost(ServerConnectionConst.Ip);
            address.Port = ServerConnectionConst.PlayerPort;

            _model.PlayerHost = new Host();
            _model.PlayerHost.Create();
            _model.PlayerPeer = _model.PlayerHost.Connect(address);

            _playerConnectionUpdater = new ServerPlayerConnectionUpdater(_gameModel);
            _gameModel.UpdatersList.Add(_playerConnectionUpdater);
        }

        private void HandlePlayerDisconnect()
        {
            _model.PlayerHost.Dispose();
            Library.Deinitialize();
            
            _gameModel.UpdatersList.Remove(_playerConnectionUpdater);
        }
        
        private void HandleWorldConnect()
        {
            Library.Initialize();

            var address = new Address();
            address.SetHost(ServerConnectionConst.Ip);
            address.Port = ServerConnectionConst.WorldPort;

            _model.WorldHost = new Host();
            _model.WorldHost.Create();
            _model.WorldPeer = _model.WorldHost.Connect(address);
            
            _worldConnectionUpdater = new ServerWorldConnectionUpdater(_gameModel);
            _gameModel.UpdatersList.Add(_worldConnectionUpdater);
        }

        private void HandleWorldDisconnect()
        {
            _model.WorldHost.Dispose();
            Library.Deinitialize();
            
            _gameModel.UpdatersList.Remove(_worldConnectionUpdater);
        }
    }
}