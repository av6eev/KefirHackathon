using ServerCore.Main.Utilities;

namespace ServerCore.Main.Commands
{
    public class EntityAnimationCommand : BaseCommand
    {
        public override string Id => "EntityAnimationCommand";
        public string PlayerId;
        public byte AnimationState;

        public EntityAnimationCommand(string playerId, EntityAnimationState animationState)
        {
            PlayerId = playerId;
            AnimationState = (byte)animationState;
        }

        public EntityAnimationCommand()
        {
        }

        public override void Read(Protocol protocol)
        {
            protocol.Get(out PlayerId);
            protocol.Get(out AnimationState);
        }

        public override void Write(Peer peer)
        {
            var protocol = new Protocol();
            var packet = default(Packet);
            
            protocol.Add(Id);
            protocol.Add(PlayerId);
            protocol.Add(AnimationState);

            packet.Create(protocol.Stream.GetBuffer());

            peer.Send(0, ref packet);
        }
    }
}