using Server.CommandExecutors;
using Server.Party.Collection;
using Server.World.Collection;
using ServerCore.Main;
using ServerCore.Main.Specifications;
using ServerCore.Main.Users.Collection;
using ServerCore.Main.Utilities.LoadWrapper.Json;
using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.Presenter;
using ServerCore.Main.World;

namespace Server;

public class Server
{
    private const ushort PlayerPort = 6005;
    private const int MaxClients = 100;

    private static Host _playerHost = new();

    private readonly PresentersList _presenters = new();
    
    public async void Start()
    {
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
        _playerHost.Create(playerAddress, MaxClients);

        var executorFactory = new ExecutorFactory();
        
        while (true)
        {
            var polled = false;

            HandleWorldTick(gameModel);
            HandleUserTick(gameModel);
            
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

            user.Write(protocol);
            
            packet.Create(protocol.Stream.GetBuffer());

            user.Peer.Send(0, ref packet);
        }
    }

    private void HandleWorldTick(ServerGameModel gameModel)
    {
        var protocol = new Protocol();
        var packet = default(Packet);

        foreach (var user in gameModel.UsersCollection.GetUsers())
        {
            var world = gameModel.WorldsCollection.Worlds[user.WorldId];
            
            if (user.FirstConnection)
            {
                Console.WriteLine("first connection: " + user.PlayerId.Value);
                
                var fullProtocol = new Protocol();
                var fullPacket = default(Packet);
                
                world.WriteAll(protocol);
                
                fullPacket.Create(fullProtocol.Stream.GetBuffer());
            
                user.Peer.Send(0, ref fullPacket);
                user.FirstConnection = false;
                
                continue;
            }

            world.Write(protocol);
            
            packet.Create(protocol.Stream.GetBuffer());

            user.Peer.Send(0, ref packet);
        }
    }
}