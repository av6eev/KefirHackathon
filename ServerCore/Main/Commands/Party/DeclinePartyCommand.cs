using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands.Party
{
    public class DeclinePartyCommand : BaseCommand
    {
        public override string Id => CommandConst.DeclineParty;

        public string PartyId;
        public string UserId;
    
        public DeclinePartyCommand(Protocol protocol) : base(protocol)
        {
        }

        public DeclinePartyCommand(string userId, string partyId)
        {
            PartyId = partyId;
            UserId = userId;
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