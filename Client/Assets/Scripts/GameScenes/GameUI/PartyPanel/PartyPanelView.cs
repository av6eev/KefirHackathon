using System.Collections.Generic;
using GameScenes.GameUI.PartyPanel.Slot;
using UnityEngine;
using UnityEngine.UI;

namespace GameScenes.GameUI.PartyPanel
{
    public class PartyPanelView : MonoBehaviour
    {
        public RectTransform Root;
        public RectTransform ContentRoot;
        public PartyPanelSlotView SlotPrefab;
        public Button LeaveButton;
    }
}