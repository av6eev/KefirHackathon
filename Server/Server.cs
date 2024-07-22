using Server.CommandExecutors;
using Server.Party.Collection;
using Server.Party.Invite.Collection;
using Server.Save.Single.Collection;
using Server.Users.Collection;
using Server.World.Collection;
using ServerCore.Main;
using ServerCore.Main.Specifications;
using ServerCore.Main.Utilities;
using ServerCore.Main.Utilities.LoadWrapper.Json;
using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;
using ServerCore.Main.World;

namespace Server;

public class Server
{
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
            PartiesCollection = new PartiesCollection(),
            PartyInviteCollection = new PartyInviteCollection(),
            SaveSingleModelCollection = new SaveSingleModelCollection()
        };
        
        _presenters.Add(new UsersCollectionPresenter(gameModel, (UsersCollection)gameModel.UsersCollection));
        _presenters.Add(new PartiesCollectionPresenter(gameModel, gameModel.PartiesCollection));
        _presenters.Add(new PartyInviteCollectionPresenter(gameModel, (PartyInviteCollection)gameModel.PartyInviteCollection));
        _presenters.Add(new SaveSingleModelCollectionPresenter(gameModel, (SaveSingleModelCollection)gameModel.SaveSingleModelCollection));
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
            Port = ServerConst.PlayerPort
        };

        _playerHost = new Host();
        _playerHost.Create(playerAddress, ServerConst.MaxClients, ServerConst.MaxChannels);

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
                        gameModel.UsersCollection.DisconnectUser(netEvent.Peer);
                        break;
                    case EventType.Timeout:
                        Console.WriteLine("[PLAYER]: Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
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
        foreach (var user in gameModel.UsersCollection.GetModels())
        {
            var protocol = new Protocol();
            var packet = default(Packet);

            if (!user.UserData.Write(protocol)) continue;
            
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
        
        foreach (var user in gameModel.UsersCollection.GetModels())
        {
            if (user.WorldFirstConnection)
            {
                var packet = new Packet(default);
                
                packet.Create(fullWorldsData[user.WorldId].Stream.GetBuffer());
                
                Console.WriteLine("First connection: " + user.PlayerId);

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