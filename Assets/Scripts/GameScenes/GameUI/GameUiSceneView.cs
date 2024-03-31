using Dialogs.Collection;
using GameScenes.GameUI.DeBuffPanel;
using Item.ItemPlaceholder;
using LocationBuilder;
using Skills.SkillPanel;

namespace GameScenes.GameUI
{
    public class GameUiSceneView : LocationSceneView
    {
        public DialogsCollectionView DialogsCollectionView;
        public ItemPlaceholderView ItemPlaceholderView;
        public SkillPanelView SkillPanelView;
        public PlayerMainResourceView AmnesiaResourceView;
        public PlayerMainResourceView HealthResourceView;
        public DeBuffPanelView DeBuffPanelView;
    }
}