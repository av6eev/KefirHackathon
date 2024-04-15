using ServerCore.Main;
using UnityEngine;
using Updater;
using EventType = ServerCore.Main.EventType;

namespace ServerManagement.Test.World
{
    public class ServerWorldConnectionUpdater : IUpdater
    {
        private readonly GameModel _gameModel;

        public ServerWorldConnectionUpdater(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Update(float deltaTime)
        {
            var client = _gameModel.ServerConnectionModel.WorldHost;
            var peer = _gameModel.ServerConnectionModel.WorldPeer;
            
            if (client.CheckEvents(out var netEvent) <= 0 && client.Service(1, out netEvent) <= 0)
            {
                return;
            }

            switch (netEvent.Type)
            {
                case EventType.None:
                    break;
                case EventType.Connect:
                    Debug.Log("[WORLD]: Client connected to server - ID: " + peer.ID);
                    _gameModel.ServerConnectionModel.CompleteWorldConnect();
                    break;
                case EventType.Disconnect:
                    Debug.Log("[WORLD]: Client disconnected from server");
                    break;
                case EventType.Timeout:
                    Debug.Log("[WORLD]: Client connection timeout");
                    break;
                case EventType.Receive:
                    var readBuffer = new byte[2048];
                    
                    netEvent.Packet.CopyTo(readBuffer);
                    
                    var protocol = new Protocol(readBuffer);
                    
                    _gameModel.WorldData.Read(protocol);
                    
                    netEvent.Packet.Dispose();
                    break;
            }
        }
    }
}