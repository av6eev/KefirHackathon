using Server.CommandExecutors;
using Server.Party.Collection;
using Server.World.Collection;
using ServerCore.Main;
using ServerCore.Main.Specifications;
using ServerCore.Main.Users.Collection;
using ServerCore.Main.Utilities.LoadWrapper.Json;
using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;
using ServerCore.Main.World;

namespace Server;

public class Server
{
    private const ushort PlayerPort = 6005;
    private const int MaxClients = 100;
    private const int MaxChannels = 5;

    private static Host _playerHost = new();

    private readonly PresentersList _presenters = new();
    
    public async void Start()
    {
        Logger.SetLogger(new ServerLogger());
        
        var loadObjectModel = new LoadObjectsModel(new JsonObjectLoadWrapper(), "../../../../ServerCore/Main/Specifications");
        var specifications = new ServerSpecifications(loadObjectModel);

        await specifications.LoadAwaiter;

        var gameModel = new ServerGameModel
        {
            Specifications = specifications,
            UsersCollection = new UsersCollection(),
            WorldsCollection = new WorldsCollection(),
            PartiesCollection = new PartiesCollection()
        };
            
        _presenters.Add(new PartiesCollectionPresenter(gameModel, gameModel.PartiesCollection));
        _presenters.Init();
        
        gameModel.WorldsCollection.Worlds.Add("hub", new WorldData("hub"));
        
        var playerThread = new Thread(() => PlayerObserve(gameModel));
        playerThread.Start();
            
        Library.Initialize();

        Console.WriteLine("Server started!");
    }

    private void PlayerObserve(ServerGameModel gameModel)
    {
        var playerAddress = new Address
        {
            Port = PlayerPort
        };

        _playerHost = new Host();
        _playerHost.Create(playerAddress, MaxClients, MaxChannels);

        var executorFactory = new ExecutorFactory();
        
        while (true)
        {
            var polled = false;

            HandleUserTick(gameModel);
            HandleWorldTick(gameModel);

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
                        Console.WriteLine("[PLAYER]: Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                        break;
                    case EventType.Disconnect:
                        Console.WriteLine("[PLAYER]: Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                        break;
                    case EventType.Timeout:
                        Console.WriteLine("[PLAYER]: Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);

                        gameModel.UsersCollection.TryGetUser(netEvent.Peer, out var user);
                        
                        var world = gameModel.WorldsCollection.Worlds[user.WorldId];
                        world.CharacterDataCollection.Remove(user.PlayerId.Value);
                        
                        gameModel.UsersCollection.Remove(user);
                        break;
                    case EventType.Receive:
                        // Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                        executorFactory.CreateExecutor(gameModel, ref netEvent)?.Execute();
                        netEvent.Packet.Dispose();
                        break;
                }

                _playerHost.Flush();
            }
        }
    }

    private void HandleUserTick(ServerGameModel gameModel)
    {
        foreach (var user in gameModel.UsersCollection.GetUsers())
        {
            var protocol = new Protocol();
            var packet = default(Packet);

            if (!user.Write(protocol)) continue;
            
            packet.Create(protocol.Stream.GetBuffer());
            
            SendPacket(user.Peer, 1, ref packet);
        }
    }

    private void HandleWorldTick(ServerGameModel gameModel)
    {
        var fullWorldsData = new Dictionary<string, Protocol>();
        var worldsData = new Dictionary<string, (bool, Protocol)>();
        
        foreach (var world in gameModel.WorldsCollection.Worlds)
        {
            var protocol = new Protocol();
            var fullProtocol = new Protocol();
            
            var uTimeSpan = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            world.Value.Time.Value = uTimeSpan.TotalMilliseconds;
            
            world.Value.Time.GetForProtocol(protocol);
            world.Value.Time.GetForProtocol(fullProtocol);

            world.Value.MessageType.Value = "new";
            world.Value.MessageType.GetForProtocol(fullProtocol);
            
            world.Value.MessageType.Value = "update";
            world.Value.MessageType.GetForProtocol(protocol);

            var changed = world.Value.Write(protocol);
            world.Value.WriteAll(fullProtocol);

            fullWorldsData.Add(world.Key, fullProtocol);
            worldsData.Add(world.Key, (changed, protocol));
        }
        
        foreach (var user in gameModel.UsersCollection.GetUsers())
        {
            if (user.WorldFirstConnection)
            {
                var packet = new Packet(default);
                
                packet.Create(fullWorldsData[user.WorldId].Stream.GetBuffer());
                
                Console.WriteLine("First connection: " + user.PlayerId.Value);

                user.WorldFirstConnection = false;
                
                SendPacket(user.Peer, 0, ref packet);
            }
            else
            {
                var data = worldsData[user.WorldId];
                
                if (data.Item1)
                {
                    var packet = new Packet(default);

                    packet.Create(data.Item2.Stream.GetBuffer());

                    SendPacket(user.Peer, 0, ref packet);
                }
            }
        }
    }

    private void SendPacket(Peer peer, byte channelId, ref Packet packet)
    {
        if (!peer.Send(channelId, ref packet))
        {   
            Console.WriteLine($"Failed to send data to peer: {peer.ID} on {channelId} channel.");
        }
    }
}