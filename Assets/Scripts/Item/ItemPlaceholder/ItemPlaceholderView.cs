using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Slot;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Item.ItemPlaceholder
{
    public class ItemPlaceholderView : MonoBehaviour, IDropHandler
    {
        public event Action<string, int> Drop; 
        
        public RectTransform Root;
        public Image IconImage;
        
        public void OnDrop(PointerEventData eventData)
        {
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);
            
            foreach (var go in result.Where(go => go.gameObject.CompareTag("ItemCell")))
            {
                var slot = go.gameObject.GetComponent<InventorySlotView>(); 
                Debug.Log(slot.Index);
                Drop?.Invoke(slot.InventoryId, slot.Index);
            }
        }
    }
}