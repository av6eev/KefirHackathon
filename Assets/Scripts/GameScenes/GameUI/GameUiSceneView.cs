using Dialogs.Collection;
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
    }
}