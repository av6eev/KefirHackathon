using System.Collections.Generic;
using GameScenes.GameUI.FriendsPanel.Slot;

namespace GameScenes.GameUI.FriendsPanel
{
    public class FriendsPanelModel
    {
        public bool IsOpen;
        public readonly Dictionary<string, FriendsPanelSlotModel> SlotModels = new();
    }
}