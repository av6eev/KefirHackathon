using UnityEngine;

namespace Item.ItemPlaceholder
{
    public interface IItemPlaceholderModel
    {
        void SetPosition(Vector2 position);
        void Show(Vector2 position, string fromInventoryId, int fromIndex);
        void Hide();
    }
}