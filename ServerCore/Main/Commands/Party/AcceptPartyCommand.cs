using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Party
{
    public class AcceptPartyCommand : BaseCommand
    {
        public override string Id => CommandConst.AcceptParty;

        public string UserId;
        public string PartyId;

        public AcceptPartyCommand(Protocol protocol) : base(protocol)
        {
        }

        public AcceptPartyCommand(string userId, string partyId)
        {
            UserId = userId;
            PartyId = partyId;
        }
    
        public override void Read(Protocol protocol)
        {
            protocol.Get(out UserId);
            protocol.Get(out PartyId);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(UserId);
            protocol.Add(PartyId);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}