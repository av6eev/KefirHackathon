using System.Collections.Generic;
using GameScenes.GameUI.PartyPanel.Slot;
using UnityEngine;

namespace GameScenes.GameUI.PartyPanel
{
    public class PartyPanelView : MonoBehaviour
    {
        public RectTransform Root;
        public RectTransform ContentRoot;
        public PartyPanelSlotView SlotPrefab;

        public readonly Dictionary<string, PartyPanelSlotView> ActiveSlots = new();
    }
}