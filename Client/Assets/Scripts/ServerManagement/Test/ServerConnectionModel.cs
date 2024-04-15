using System;
using Awaiter;
using ServerCore.Main;

namespace ServerManagement.Test
{
    public class ServerConnectionModel : IServerConnectionModel
    {
        public event Action OnPlayerConnect;
        public event Action OnWorldConnect;
        public event Action OnPlayerDisconnect;
        public event Action OnWorldDisconnect;

        public CustomAwaiter CompletePlayerConnectAwaiter = new();
        public CustomAwaiter CompleteWorldConnectAwaiter = new();
        
        public Peer PlayerPeer { get; set; }
        public Peer WorldPeer { get; set; }
        public Host PlayerHost { get; set; }
        public Host WorldHost { get; set; }
        
        public void ConnectPlayer()
        {
            OnPlayerConnect?.Invoke();
        }
        
        public void ConnectWorld()
        {
            OnWorldConnect?.Invoke();
        }
        
        public void DisconnectPlayer()
        {
            OnPlayerDisconnect?.Invoke();
        }
        
        public void DisconnectWorld()
        {
            OnWorldDisconnect?.Invoke();
        }
        
        public void CompletePlayerConnect()
        {
            CompletePlayerConnectAwaiter.Complete();
            CompletePlayerConnectAwaiter = new CustomAwaiter();
        }
        
        public void CompleteWorldConnect()
        {
            CompleteWorldConnectAwaiter.Complete();
            CompleteWorldConnectAwaiter = new CustomAwaiter();
        }
    }
}