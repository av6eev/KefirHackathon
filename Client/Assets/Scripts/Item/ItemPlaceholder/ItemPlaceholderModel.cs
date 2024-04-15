using Reactive.Event;
using Reactive.Field;
using UnityEngine;

namespace Item.ItemPlaceholder
{
    public class ItemPlaceholderModel : IItemPlaceholderModel
    {
        public ReactiveEvent ShowEvent { get; } = new();
        public ReactiveEvent HideEvent { get; } = new();
        
        public ReactiveField<Vector2> Position { get; } = new();

        public string FromInventoryId;
        public int FromIndex;
        
        public void SetPosition(Vector2 position)
        {
            Position.Value = position;
        }

        public void Show(Vector2 position, string fromInventoryId, int fromIndex)
        {
            Position.Value = position;
            FromInventoryId = fromInventoryId;
            FromIndex = fromIndex;
            
            ShowEvent.Invoke();
        }
        
        public void Hide()
        {
            HideEvent.Invoke();
        }
    }
}