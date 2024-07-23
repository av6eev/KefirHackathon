using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Friends
{
    public class InviteFriendCommand : BaseCommand
    {
        public override string Id => CommandConst.InviteFriend;

        public string FromUserId;
        public string InvitedUserId;

        public InviteFriendCommand(string fromUserId, string invitedUserId)
        {
            FromUserId = fromUserId;
            InvitedUserId = invitedUserId;
        }

        public InviteFriendCommand(Protocol protocol) : base(protocol)
        {
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