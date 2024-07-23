using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Party
{
    public class InvitePartyCommand : BaseCommand
    {
        public override string Id => CommandConst.InviteParty;

        public string FromUserId;
        public string InvitedUserId;

        public InvitePartyCommand(Protocol protocol) : base(protocol)
        {
        }

        public InvitePartyCommand(string fromUserId, string invitedUserId)
        {
            FromUserId = fromUserId;
            InvitedUserId = invitedUserId;
        }
    
        public override void Read(Protocol protocol)
        {
            protocol.Get(out FromUserId);
            protocol.Get(out InvitedUserId);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(FromUserId);
            protocol.Add(InvitedUserId);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}