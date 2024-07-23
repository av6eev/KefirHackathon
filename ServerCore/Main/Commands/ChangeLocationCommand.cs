using ServerCore.Main.Utilities;
using ServerCore.Main.Utilities.Awaiter;

namespace ServerCore.Main.Commands
{
    public class ChangeLocationCommand : BaseCommand
    {
        public override string Id => CommandConst.ChangeLocation;

        public string PlayerId;
        public string ToId;

        public ChangeLocationCommand(string playerId, string toId)
        {
            PlayerId = playerId;
            ToId = toId;
        }

        public ChangeLocationCommand(Protocol protocol) : base(protocol)
        {
        
        }

        public override void Read(Protocol protocol)
        {
            protocol.Get(out PlayerId);
            protocol.Get(out ToId);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(PlayerId);
            protocol.Add(ToId);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}