using Newtonsoft.Json;
using ServerCore.Main;
using ServerCore.Main.Utilities;
using UnityEngine;
using Updater;
using EventType = ServerCore.Main.EventType;

namespace ServerManagement.Test.Player
{
    public class ServerPlayerConnectionUpdater : IUpdater
    {
        private readonly GameModel _gameModel;

        public ServerPlayerConnectionUpdater(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Update(float deltaTime)
        {
            var client = _gameModel.ServerConnectionModel.PlayerHost;
            var peer = _gameModel.ServerConnectionModel.PlayerPeer;

            if (client.CheckEvents(out var netEvent) <= 0 && client.Service(0, out netEvent) <= 0) return;
            
            switch (netEvent.Type)
            {
                case EventType.None:
                    break;
                case EventType.Connect:
                    Debug.Log("[PLAYER]: Client connected to server - ID: " + peer.ID);
                    _gameModel.ServerConnectionModel.CompletePlayerConnect();
                    break;
                case EventType.Disconnect:
                    Debug.Log("[PLAYER]: Client disconnected from server");
                    break;
                case EventType.Timeout:
                    Debug.Log("[PLAYER]: Client connection timeout");
                    break;
                case EventType.Receive:
                    var readBuffer = new byte[2048*2];

                    netEvent.Packet.CopyTo(readBuffer);
                    var protocol = new Protocol(readBuffer);

                    if (netEvent.ChannelID == 0)
                    {
                        var time = _gameModel.WorldData.Time.Value;
                        _gameModel.WorldData.Time.SetFromProtocol(protocol, out var test);
                        
                        if (_gameModel.WorldData.Time.Value != time)
                        {
                            _gameModel.WorldData.MessageType.SetFromProtocol(protocol, out var messageType);

                            if (_gameModel.WorldData.MessageType.Value == "new")
                            {
                                _gameModel.WorldData.CharacterDataCollection.Collection.Clear();
                            }
                            
                            var readData = _gameModel.WorldData.Read(protocol);
                            Debug.Log("[WORLD DATA]: " + JsonConvert.SerializeObject(readData));    
                        }
                    }
                    else if (netEvent.ChannelID == 1)
                    {
                        var readData = _gameModel.PlayerModel.UserData.Read(protocol);
                        Debug.Log("[USER DATA]: " + JsonConvert.SerializeObject(readData));
                    }

                    netEvent.Packet.Dispose();
                    break;
            }
        }
    }
}