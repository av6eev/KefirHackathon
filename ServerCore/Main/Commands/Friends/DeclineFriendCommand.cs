using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Friends
{
    public class DeclineFriendCommand : BaseCommand
    {
        public override string Id => CommandConst.DeclineFriend;
    
        public string InviteId;
        public string UserId;
    
        public DeclineFriendCommand(Protocol protocol) : base(protocol)
        {
        }

        public DeclineFriendCommand(string userId, string inviteId)
        {
            InviteId = inviteId;
            UserId = userId;
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