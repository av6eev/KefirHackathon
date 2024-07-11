using Reactive.Field;

namespace GameScenes.GameUI.PartyPanel.Slot
{
    public class PartyPanelSlotModel
    {
        public readonly ReactiveField<bool> IsOwner = new();
        public readonly string UserName;

        public PartyPanelSlotModel(string userName)
        {
            UserName = userName;
        }
    }
}