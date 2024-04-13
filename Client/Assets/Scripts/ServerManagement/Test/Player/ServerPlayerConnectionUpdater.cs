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
            
            if (client.CheckEvents(out var netEvent) <= 0 && client.Service(15, out netEvent) <= 0)
            {
                return;
            }

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
                    Debug.Log("[PLAYER]: Packet received from server - Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                    netEvent.Packet.Dispose();
                    break;
            }
        }
    }
}