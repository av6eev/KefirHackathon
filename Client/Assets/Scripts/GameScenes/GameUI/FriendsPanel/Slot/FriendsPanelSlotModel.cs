using Reactive.Field;

namespace GameScenes.GameUI.FriendsPanel.Slot
{
    public class FriendsPanelSlotModel
    {
        public readonly ReactiveField<bool> IsOnline = new();
        public readonly string UserName;

        public FriendsPanelSlotModel(string userName, bool isOnline)
        {
            UserName = userName;
            IsOnline.Value = isOnline;
        }
    }
}