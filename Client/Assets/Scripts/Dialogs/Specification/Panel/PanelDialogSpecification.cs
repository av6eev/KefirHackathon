using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Dialogs.Specification.Panel
{
    [Serializable]
    public class PanelDialogSpecification : DialogSpecification
    {
        public List<PanelButtonSpecification> Buttons;
        public Button ButtonPrefab;
    }
}