using System.Diagnostics;
using ENet;
using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Property;
using ServerCore.Main.Utilities;
using Address = ServerCore.Main.Address;
using Event = ServerCore.Main.Event;
using EventType = ServerCore.Main.EventType;
using Host = ServerCore.Main.Host;
using Library = ServerCore.Main.Library;
using Peer = ServerCore.Main.Peer;
using Protocol = ServerCore.Main.Protocol;

namespace Server
{
    public class Program
    {
        private const ushort WorldPort = 6006;
        private const ushort PlayerPort = 6005;
        private const int MaxClients = 100;

        private static int _currentTick;
        private static float _timer;
        
        private static Host _playerHost = new();
        private static Host _worldHost = new();
        
        private static void Main(string[] args)
        {
            var gameModel = new ServerGameModel();
            var worldThread = new Thread(() => WorldObserve(gameModel));
            var playerThread = new Thread(() => PlayerObserve(gameModel));

            worldThread.Start();
            playerThread.Start();
            
            Library.Initialize();

            Console.WriteLine("STARTED");
            
            while (true)
            {
            }            

            Library.Deinitialize();
        }

        private static void WorldObserve(ServerGameModel gameModel)
        {
            var worldAddress = new Address
            {
                Port = WorldPort
            };

            _worldHost = new Host();
            _worldHost.Create(worldAddress, MaxClients);

            var peers = new List<ServerCore.Main.Peer>();
            
            while (true)
            {
                var polled = false;

                while (!polled)
                {
                    if (_worldHost.CheckEvents(out var netEvent) <= 0)
                    {
                        if (_worldHost.Service(15, out netEvent) <= 0)
                            break;

                        polled = true;
                    }

                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;
                        case EventType.Connect:
                            Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            
                            peers.Add(netEvent.Peer);
                            break;
                        case EventType.Disconnect:
                            Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Timeout:
                            Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                    }

                    _worldHost.Flush();
                }

                _timer += .02f;
                
                while (_timer >= ServerConst.TimeBetweenTicks)
                {
                    _timer -= ServerConst.TimeBetweenTicks;
                    
                    HandleWorldTick(gameModel, ref peers);
                    _currentTick++;
                }
            }
        }

        private static void HandleWorldTick(ServerGameModel gameModel, ref List<Peer> peers)
        {
            var protocol = new Protocol();
            var packet = default(ServerCore.Main.Packet);

            if (!gameModel.WorldData.Write(protocol))
            {
                return;
            }
            
            packet.Create(protocol.Stream.GetBuffer());

            foreach (var peer in peers)
            {
                peer.Send(0, ref packet);
            }
        }

        private static void PlayerObserve(ServerGameModel gameModel)
        {
            var playerAddress = new Address
            {
                Port = PlayerPort
            };
            
            _playerHost = new Host();
            _playerHost.Create(playerAddress, MaxClients);
            
            while (true)
            {
                var polled = false;

                while (!polled)
                {
                    if (_playerHost.CheckEvents(out var netEvent) <= 0)
                    {
                        if (_playerHost.Service(15, out netEvent) <= 0)
                            break;

                        polled = true;
                    }

                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;
                        case EventType.Connect:
                            Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Disconnect:
                            Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Timeout:
                            Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Receive:
                            // Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                            HandleCommand(gameModel, ref netEvent);
                            netEvent.Packet.Dispose();
                            break;
                    }

                    _playerHost.Flush();
                }
            }
        }

        private static void HandleCommand(ServerGameModel gameModel, ref Event netEvent)
        {
            var readBuffer = new byte[1024];

            netEvent.Packet.CopyTo(readBuffer);
            
            var protocol = new Protocol(readBuffer);
            protocol.Get(out string command);

            switch (command)
            {
                case "PlayerMovementCommand":
                    var playerMovementCommand = new PlayerMovementCommand();
                    playerMovementCommand.Read(protocol);

                    if (gameModel.WorldData.Dataset.TryGetValue(playerMovementCommand.PlayerId, out var value))
                    {
                        var serverData = (CharacterServerData)value;

                        serverData.LatestServerPosition.Value = new Vector3(playerMovementCommand.X, playerMovementCommand.Y, playerMovementCommand.Z);
                        serverData.CurrentTick.Value = playerMovementCommand.Tick++;
                        serverData.Rotation.Value = playerMovementCommand.RotationY;
                        serverData.Speed.Value = playerMovementCommand.Speed;

                        // serverData.Position.Value = new Vector3(playerMovementCommand.X, playerMovementCommand.Y, playerMovementCommand.Z);
                    }
                    break;
                case "LoginCommand":
                    var loginCommand = new LoginCommand();
                    loginCommand.Read(protocol);
                    
                    gameModel.WorldData.Dataset.Add(loginCommand.PlayerId, new CharacterServerData(loginCommand.PlayerId));
                    break;
                case "EntityAnimationCommand":
                    var entityAnimationCommand = new EntityAnimationCommand();
                    entityAnimationCommand.Read(protocol);
                    
                    if (gameModel.WorldData.Dataset.TryGetValue(entityAnimationCommand.PlayerId, out var entityServerData))
                    {
                        var newAnimationState = entityAnimationCommand.AnimationState;
                        var serverData = (CharacterServerData)entityServerData;
                        
                        serverData.AnimationState.Value = newAnimationState;    
                    }
                    break;
            }
        }
    }
}