namespace ServerCore.Main.Commands
{
    public interface ICommand
    {
        void Read(Protocol protocol);
        void Write(Peer peer);
    }
}