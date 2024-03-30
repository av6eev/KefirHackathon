using Reactive.Event;

namespace Utilities.ModelCollection
{
    public interface ICollection
    {
        ReactiveEvent ChangeEvent { get; }
    }
}