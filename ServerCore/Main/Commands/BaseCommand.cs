namespace ServerCore.Main.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Id { get; }

        public abstract void Read(Protocol protocol);
        public abstract void Write(Peer peer);
    }
}