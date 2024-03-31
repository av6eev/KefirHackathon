using Quest;
using UnityEngine;

namespace GameScenes.GameUI.QuestPanel
{
    public class QuestPanelView : MonoBehaviour
    {
        public RectTransform Root;
        public Transform ContentRoot;
        public QuestView QuestPrefab;
        public GameObject NoActiveQuests;
    }
}