using Reactive.Event;
using Reactive.Field;
using UnityEngine;
using Utilities.Model;

namespace Entities
{
    public class EntityResource : IModel
    {
        private readonly ReactiveEvent _saveEvent = new();
        
        public int MaxAmount { get; }
        public int MinAmount { get; }
        public ReactiveField<float> Amount { get; } = new();
        public readonly EntityResourceType Type;

        public EntityResource(EntityResourceType type, int amount, int maxAmount = -1, int minAmount = -1)
        {
            Type = type;
            Amount.Value = amount;
            MaxAmount = maxAmount;
            MinAmount = minAmount;
        }

        public void Increase(float amount)
        {
            if (MaxAmount != -1 && Amount.Value + amount > MaxAmount)
            {
                Amount.Value = MaxAmount;
            }
            else
            {
                Amount.Value += amount;
            }
            
            _saveEvent.Invoke();
        }
        
        public void Decrease(float amount)
        {
            if (MinAmount != -1 && Amount.Value - amount < MinAmount)
            {
                Amount.Value = MinAmount;
            }
            else
            {
                Amount.Value -= amount;
            }
            
            _saveEvent.Invoke();
        }
    }
}