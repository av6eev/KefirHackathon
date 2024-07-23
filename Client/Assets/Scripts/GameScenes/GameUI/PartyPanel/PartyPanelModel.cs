using System.Collections.Generic;
using GameScenes.GameUI.PartyPanel.Slot;

namespace GameScenes.GameUI.PartyPanel
{
    public class PartyPanelModel
    {
        public readonly Dictionary<string, PartyPanelSlotModel> SlotModels = new();
    }
}