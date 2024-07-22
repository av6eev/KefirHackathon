using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Friends
{
    public class AcceptFriendCommand : BaseCommand
    {
        public override string Id => CommandConst.AcceptFriend;
    
        public string UserId;
        public string InviteId;

        public AcceptFriendCommand(Protocol protocol) : base(protocol)
        {
        }

        public AcceptFriendCommand(string userId, string inviteId)
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