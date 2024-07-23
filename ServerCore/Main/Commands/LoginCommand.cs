using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands
{
    public class LoginCommand : BaseCommand
    {
        public override string Id => CommandConst.Login;

        public string PlayerId;
        public string PlayerNickname;

        public LoginCommand(string playerId, string playerNickname)
        {
            PlayerId = playerId;
            PlayerNickname = playerNickname;
        }

        public LoginCommand(Protocol protocol) : base(protocol)
        {
        }

        public override void Read(Protocol protocol)
        {
            protocol.Get(out PlayerId);
            protocol.Get(out PlayerNickname);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(PlayerId);
            protocol.Add(PlayerNickname);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}