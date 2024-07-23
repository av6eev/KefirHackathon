namespace ServerCore.Main.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Id { get; }

        protected BaseCommand(Protocol protocol)
        {
            Read(protocol);
        }

        protected BaseCommand() {}
        
        public abstract void Read(Protocol protocol);
        public abstract void Write(Peer peer);
    }
}