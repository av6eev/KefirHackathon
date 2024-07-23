using ServerCore.Main.Utilities.Event;

namespace Server.Utilities.ModelCollection
{
    public interface ICollection
    {
        ReactiveEvent ChangeEvent { get; }
    }
}