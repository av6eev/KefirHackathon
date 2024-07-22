using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Friends
{
    public class RemoveFriendCommand : BaseCommand
    {
        public override string Id => CommandConst.RemoveFriend;
    
        public string FromUserId;
        public string RemovedUserNickname;

        public RemoveFriendCommand(string fromUserId, string removedNickname)
        {
            FromUserId = fromUserId;
            RemovedUserNickname = removedNickname;
        }

        public RemoveFriendCommand(Protocol protocol) : base(protocol)
        {
        }
    
        public override void Read(Protocol protocol)
        {
            protocol.Get(out FromUserId);
            protocol.Get(out RemovedUserNickname);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(FromUserId);
            protocol.Add(RemovedUserNickname);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}