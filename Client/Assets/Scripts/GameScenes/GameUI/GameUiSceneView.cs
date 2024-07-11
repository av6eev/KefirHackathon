using Dialogs.Collection;
using GameScenes.GameUI.DeBuffPanel;
using GameScenes.GameUI.DebugPanel;
using GameScenes.GameUI.PartyPanel;
using GameScenes.GameUI.QuestPanel;
using Item.ItemPlaceholder;
using LoadingScreen;
using LocationBuilder;
using Skills.SkillPanel;

namespace GameScenes.GameUI
{
    public class GameUiSceneView : LocationSceneView
    {
        public DialogsCollectionView DialogsCollectionView;
        public ItemPlaceholderView ItemPlaceholderView;
        public SkillPanelView SkillPanelView;
        public QuestPanelView QuestPanelView;
        public PlayerMainResourceView AmnesiaResourceView;
        public PlayerMainResourceView HealthResourceView;
        public DeBuffPanelView DeBuffPanelView;
        public DebugPanelView DebugPanelView;
        public LoadingScreenView LoadingScreenView;
        public PartyPanelView PartyPanelView;
    }
}