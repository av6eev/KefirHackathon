using System;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.Slot
{
    public class InventorySlotView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action<Vector2> BeginDrag;
        public event Action EndDrag;
        public event Action<Vector2> Drag;
        
        [NonSerialized] public int Index;
        [NonSerialized] public string InventoryId;
        [NonSerialized] public bool NeedType;
        [NonSerialized] public ItemType ItemType;
        
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI AmountText;
        
        public Image IconImage;
        public Image DefaultBorderImage;
        public Image HighlightBorderImage;
        
        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(eventData.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke();
        }
    }
}