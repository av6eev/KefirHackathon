using Dialogs.Collection;
using Item.ItemPlaceholder;
using LocationBuilder;
using PlayerInventory.Hud;

namespace GameScenes.GameUI
{
    public class GameUiSceneView : LocationSceneView
    {
        public DialogsCollectionView DialogsCollectionView;
        public ItemPlaceholderView ItemPlaceholderView;
        public PlayerInventoryHudView PlayerInventoryHudView;
    }
}