using System;
using Awaiter;
using ServerCore.Main;

namespace ServerManagement.Test
{
    public class ServerConnectionModel : IServerConnectionModel
    {
        public event Action OnPlayerConnect;
        public event Action OnPlayerDisconnect;

        public CustomAwaiter CompletePlayerConnectAwaiter = new();
        
        public Peer PlayerPeer { get; set; }
        public Host PlayerHost { get; set; }
        
        public void ConnectPlayer()
        {
            OnPlayerConnect?.Invoke();
        }
        
        public void DisconnectPlayer()
        {
            OnPlayerDisconnect?.Invoke();
        }
        
        public void CompletePlayerConnect()
        {
            CompletePlayerConnectAwaiter.Complete();
            CompletePlayerConnectAwaiter = new CustomAwaiter();
        }
    }
}