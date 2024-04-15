// using System.Collections.Generic;
// using System.IO;
// using Entities.Player;
// using UnityEngine;
//
// namespace ServerManagement
// {
//     public class ServerConnection : MonoBehaviour
//     {
//         private Host _client;
//         private Peer _peer;
//         private int _skipFrame;
//         
//         public PlayerView PlayerPrefab;
//         public PlayerView OtherPlayerPrefab;
//
//         private PlayerView _myPlayer;
//         private uint _myPlayerId;
//         
//         private readonly Dictionary<uint, PlayerView> _players = new();
//
//         private void Start()
//         {
//             Application.runInBackground = true;
//             
//             InitENet();
//             
//             _myPlayer = Instantiate(PlayerPrefab);
//         }
//
//         private void FixedUpdate()
//         {
//             UpdateENet();
//
//             if (++_skipFrame < 3)
//             {
//                 return;
//             }
//
//             SendPositionUpdate();
//             _skipFrame = 0;
//         }
//
//         private void InitENet()
//         {
//             Library.Initialize();
//
//             var address = new Address();
//             address.SetHost(IP);
//             address.Port = Port;
//
//             _client = new Host();
//             _client.Create();
//             
//             Debug.Log("Connecting");
//             
//             _peer = _client.Connect(address);
//         }
//
//         private void UpdateENet()
//         {
//             if (_client.CheckEvents(out var netEvent) <= 0)
//             {
//                 if (_client.Service(15, out netEvent) <= 0)
//                     return;
//             }
//
//             switch (netEvent.Type)
//             {
//                 case EventType.None:
//                     break;
//                 case EventType.Connect:
//                     Debug.Log("Client connected to server - ID: " + _peer.ID);
//                     SendLogin();
//                     break;
//                 case EventType.Disconnect:
//                     Debug.Log("Client disconnected from server");
//                     break;
//                 case EventType.Timeout:
//                     Debug.Log("Client connection timeout");
//                     break;
//                 case EventType.Receive:
//                     Debug.Log("Packet received from server - Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
//                     ParsePacket(ref netEvent);
//                     netEvent.Packet.Dispose();
//                     break;
//             }
//         }
//
//         private void SendPositionUpdate()
//         {
//             var x = _myPlayer.Position.x;
//             var y = _myPlayer.Position.z;
//
//             var protocol = new Protocol();
//             var buffer = protocol.Serialize((byte)PacketId.PositionUpdateRequest, _myPlayerId, x, y);
//             var packet = default(Packet);
//             packet.Create(buffer);
//             
//             _peer.Send(ChannelID, ref packet);
//         }
//
//         private void SendLogin()
//         {
//             Debug.Log("SendLogin");
//             
//             var protocol = new Protocol();
//             var buffer = protocol.Serialize((byte)PacketId.LoginRequest, 0);
//             var packet = default(Packet);
//             
//             packet.Create(buffer);
//             _peer.Send(ChannelID, ref packet);
//         }
//
//         private void ParsePacket(ref Event netEvent)
//         {
//             var readBuffer = new byte[1024];
//             var readStream = new MemoryStream(readBuffer);
//             var reader = new BinaryReader(readStream);
//
//             readStream.Position = 0;
//             netEvent.Packet.CopyTo(readBuffer);
//             var packetId = (PacketId)reader.ReadByte();
//
//             Debug.Log("ParsePacket received: " + packetId);
//
//             switch (packetId)
//             {
//                 case PacketId.LoginResponse:
//                     _myPlayerId = reader.ReadUInt32();
//                     Debug.Log("MyPlayerId: " + _myPlayerId);
//                     break;
//                 case PacketId.LoginEvent:
//                 {
//                     var playerId = reader.ReadUInt32();
//                     Debug.Log("OtherPlayerId: " + playerId);
//                     
//                     SpawnOtherPlayer(playerId);
//                     break;
//                 }
//                 case PacketId.PositionUpdateEvent:
//                 {
//                     var playerId = reader.ReadUInt32();
//                     var x = reader.ReadSingle();
//                     var y = reader.ReadSingle();
//                     
//                     UpdatePosition(playerId, x, y);
//                     break;
//                 }
//                 case PacketId.LogoutEvent:
//                 {
//                     var playerId = reader.ReadUInt32();
//                     
//                     if (_players.ContainsKey(playerId))
//                     {
//                         Destroy(_players[playerId]);
//                         _players.Remove(playerId);
//                     }
//                     break;
//                 }
//             }
//         }
//
//         private void SpawnOtherPlayer(uint playerId)
//         {
//             if (playerId == _myPlayerId)
//             {
//                 return;
//             }
//             
//             var newPlayer = Instantiate(OtherPlayerPrefab);
//             newPlayer.Move(newPlayer.Position + new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f)));
//             
//             Debug.Log("Spawn other object " + playerId);
//             _players[playerId] = newPlayer;
//         }
//
//         private void UpdatePosition(uint playerId, float x, float y)
//         {
//             if (playerId == _myPlayerId)
//             {
//                 return;
//             }
//
//             Debug.Log("UpdatePosition " + playerId);
//             _players[playerId].Move(new Vector3(x, 0, y));
//         }
//
//         private void OnDestroy()
//         {
//             _client.Dispose();
//             Library.Deinitialize();
//         }
//     }
// }