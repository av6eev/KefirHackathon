using Reactive.Event;
using Reactive.Field;
using Utilities.Model;

namespace Entities
{
    public class EntityResource : IModel
    {
        public readonly EntityResourceType Type;
        
        public ReactiveField<int> Amount { get; } = new();
        private readonly ReactiveEvent _saveEvent;

        public EntityResource(EntityResourceType type, int amount, ReactiveEvent saveEvent)
        {
            Type = type;
            Amount.Value = amount;
            _saveEvent = saveEvent;
        }

        public void Increase(int amount)
        {
            Amount.Value += amount;
            
            _saveEvent.Invoke();
        }
        
        public void Decrease(int amount)
        {
            Amount.Value -= amount;

            if (Amount.Value < 0)
            {
                Amount.Value = 0;
            }
            
            _saveEvent.Invoke();
        }
    }
}