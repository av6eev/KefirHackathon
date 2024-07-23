using Presenter;
using ServerCore.Main;
using ServerCore.Main.Utilities;
using ServerManagement.Test.Player;
using Updater;

namespace ServerManagement.Test
{
    public class ServerConnectionPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly ServerConnectionModel _model;
        private readonly NetworkUpdaters _networkUpdaters;
        private IUpdater _playerConnectionUpdater;
        private IUpdater _worldConnectionUpdater;

        public ServerConnectionPresenter(GameModel gameModel, ServerConnectionModel model, NetworkUpdaters networkUpdaters)
        {
            _gameModel = gameModel;
            _model = model;
            _networkUpdaters = networkUpdaters;
        }
        
        public void Init()
        {
            _model.OnPlayerConnect += HandlePlayerConnect;
            _model.OnPlayerDisconnect += HandlePlayerDisconnect;
        }
        
        public void Dispose()
        {
            _model.OnPlayerConnect -= HandlePlayerConnect;
            _model.OnPlayerDisconnect -= HandlePlayerDisconnect;
        }

        private void HandlePlayerConnect()
        {
            var address = new Address();
            address.SetHost(ServerConst.LocalIp);
            address.Port = ServerConst.PlayerPort;

            _model.PlayerHost = new Host();
            _model.PlayerHost.Create();
            _model.PlayerPeer = _model.PlayerHost.Connect(address, ServerConst.MaxChannels);

            _playerConnectionUpdater = new ServerPlayerConnectionUpdater(_gameModel);
            _networkUpdaters.UpdatersList.Add(_playerConnectionUpdater);
        }

        private void HandlePlayerDisconnect()
        {
            _model.PlayerHost.Dispose();
            
            _networkUpdaters.UpdatersList.Remove(_playerConnectionUpdater);
        }
    }
}