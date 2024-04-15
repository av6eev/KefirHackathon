using System;

namespace ServerCore.Main.Commands
{
    public class PlayerMovementCommand : BaseCommand
    {
        public override string Id => "PlayerMovementCommand";

        public float Speed;
        public int X;
        public int Y;
        public int Z;
        public int RotationY;
        public int Tick;

        public string PlayerId;
        
        public PlayerMovementCommand()
        {
            
        }
        
        public PlayerMovementCommand(string playerId, float speed, int x, int y, int z, int rotationY, int tick)
        {
            PlayerId = playerId;
            Speed = speed;
            X = x;
            Y = y;
            Z = z;
            RotationY = rotationY;
            Tick = tick;
        }

        public override void Read(Protocol protocol)
        {
            protocol.Get(out PlayerId);
            protocol.Get(out Speed);
            protocol.Get(out X);
            protocol.Get(out Y);
            protocol.Get(out Z);
            protocol.Get(out RotationY);
            protocol.Get(out Tick);
            
            // Console.WriteLine($"{X},{Y},{Z}");
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(PlayerId);
            protocol.Add(Speed);
            protocol.Add(X);
            protocol.Add(Y);
            protocol.Add(Z);
            protocol.Add(RotationY);
            protocol.Add(Tick);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}