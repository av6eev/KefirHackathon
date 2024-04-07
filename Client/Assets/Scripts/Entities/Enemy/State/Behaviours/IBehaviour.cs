using Reactive.Field;

namespace Entities.Enemy.State.Behaviours
{
    public interface IBehaviour
    {
        ReactiveField<bool> IsCompleted { get; }
        void Init();
        void Dispose();
    }
}