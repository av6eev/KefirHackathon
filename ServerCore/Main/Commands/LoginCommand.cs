using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands
{
    public class LoginCommand : BaseCommand
    {
        public override string Id => CommandConst.Login;

        public string PlayerId;

        public LoginCommand(Protocol protocol) : base(protocol)
        {
        }

        public LoginCommand(string playerId)
        {
            PlayerId = playerId;
        }
    
        public override void Read(Protocol protocol)
        {
            protocol.Get(out PlayerId);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(PlayerId);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}