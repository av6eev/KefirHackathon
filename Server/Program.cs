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
        private const ushort PlayerPort = 6005;
        private const int MaxClients = 100;

        private static int _currentTick;
        private static float _timer;
        
        private static Host _playerHost = new();
        private static Dictionary<Peer, UserConnection> _users = new();
        
        private static void Main(string[] args)
        {
            var gameModel = new ServerGameModel();
            var playerThread = new Thread(() => PlayerObserve(gameModel));

            playerThread.Start();
            
            Library.Initialize();

            Console.WriteLine("STARTED");
            
            while (true)
            {
            }            

            Library.Deinitialize();
        }

        private static void HandleWorldTick(ServerGameModel gameModel, ref Dictionary<Peer, UserConnection> users)
        {
            var firstConnectionUsers = users.Values.Where(user => user.FirstConnection).ToList();
            var protocol = new Protocol();
            var packet = default(ServerCore.Main.Packet);
            
            if (gameModel.WorldData.Write(protocol))
            {
                packet.Create(protocol.Stream.GetBuffer());

                foreach (var user in users.Values.Where(user => !firstConnectionUsers.Contains(user)))
                {
                    user.Peer.Send(0, ref packet);
                }
            }
            
            foreach (var user in firstConnectionUsers)
            {
                var fullProtocol = new Protocol();
                var fullPacket = default(ServerCore.Main.Packet);

                gameModel.WorldData.WriteAll(fullProtocol);

                fullPacket.Create(fullProtocol.Stream.GetBuffer());
                user.Peer.Send(0, ref fullPacket);
                
                user.FirstConnection = false;
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
                
                HandleWorldTick(gameModel, ref _users);

                while (!polled)
                {
                    if (_playerHost.CheckEvents(out var netEvent) <= 0)
                    {
                        if (_playerHost.Service(5, out netEvent) <= 0)
                            break;

                        polled = true;
                    }

                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;
                        case EventType.Connect:
                            Console.WriteLine("[PLAYER]: Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Disconnect:
                            Console.WriteLine("[PLAYER]: Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            break;
                        case EventType.Timeout:
                            Console.WriteLine("[PLAYER]: Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                            
                            gameModel.WorldData.CharacterDataCollection.Remove(_users[netEvent.Peer].PlayerId);
                            _users.Remove(netEvent.Peer);
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

                    if (gameModel.WorldData.CharacterDataCollection.Collection.TryGetValue(playerMovementCommand.PlayerId, out var value))
                    {
                        value.LatestServerPosition.Value = new Vector3(playerMovementCommand.X, playerMovementCommand.Y, playerMovementCommand.Z);
                        value.CurrentTick.Value = playerMovementCommand.Tick++;
                        value.Rotation.Value = playerMovementCommand.RotationY;
                        value.Speed.Value = playerMovementCommand.Speed;

                        // serverData.Position.Value = new Vector3(playerMovementCommand.X, playerMovementCommand.Y, playerMovementCommand.Z);
                    }
                    break;
                case "LoginCommand":
                    var loginCommand = new LoginCommand();
                    loginCommand.Read(protocol);

                    var serverData = new CharacterServerData
                    {
                        PlayerId = { Value = loginCommand.PlayerId }
                    };

                    gameModel.WorldData.CharacterDataCollection.Add(loginCommand.PlayerId, serverData);
                    _users.Add(netEvent.Peer, new UserConnection(netEvent.Peer, loginCommand.PlayerId));
                    break;
                case "EntityAnimationCommand":
                    var entityAnimationCommand = new EntityAnimationCommand();
                    entityAnimationCommand.Read(protocol);
                    
                    if (gameModel.WorldData.CharacterDataCollection.Collection.TryGetValue(entityAnimationCommand.PlayerId, out var entityServerData))
                    {
                        entityServerData.AnimationState.Value = entityAnimationCommand.AnimationState;    
                    }
                    break;
            }
        }
    }
}