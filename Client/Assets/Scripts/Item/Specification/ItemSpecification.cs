using System;
using Specification;
using UnityEngine;

namespace Item.Specification
{
    [Serializable]
    public class ItemSpecification : BaseSpecification
    {
        public string Name;
        public string Description;
        public int MaxSlotCapacity;
        public Sprite IconSprite;
        
        public ItemType ItemType;
        
        public string PrefabId;
        public float ApplyValue;
    }
}