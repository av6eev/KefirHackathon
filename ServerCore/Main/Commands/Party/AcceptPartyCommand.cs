using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Party
{
    public class AcceptPartyCommand : BaseCommand
    {
        public override string Id => CommandConst.AcceptParty;

        public string UserId;
        public string InviteId;

        public AcceptPartyCommand(Protocol protocol) : base(protocol)
        {
        }

        public AcceptPartyCommand(string userId, string inviteId)
        {
            UserId = userId;
            InviteId = inviteId;
        }
    
        public override void Read(Protocol protocol)
        {
            protocol.Get(out UserId);
            protocol.Get(out InviteId);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(UserId);
            protocol.Add(InviteId);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}