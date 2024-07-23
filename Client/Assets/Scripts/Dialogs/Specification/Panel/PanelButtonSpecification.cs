using System;
using TMPro;
using UnityEngine;

namespace Dialogs.Specification.Panel
{
    [Serializable]
    public class PanelButtonSpecification
    {
        public string ActionId;
        public string DescriptionText;
        public float Width;
        public float Height;
        public Color Color;
        public Color TextColor;
        public float FontSize;
        public TMP_FontAsset Font;
    }
}