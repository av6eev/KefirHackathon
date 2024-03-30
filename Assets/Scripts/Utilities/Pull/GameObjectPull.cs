using Pulls;
using UnityEngine;

namespace Utilities.Pull
{
    public class GameObjectPull : Pull<GameObject>
    {
        private readonly Transform _root;
        public readonly GameObject Element;

        public GameObjectPull(GameObject element, Transform root, int minCount)
        {
            Element = element;
            _root = root;
            Init(minCount);
        }
        
        protected override GameObject CreateElement()
        {
            var go = Object.Instantiate(Element, _root);
            ModifyPutObject(go);
            return go;
        }

        protected override void RemoveElement(GameObject element)
        {
            Object.Destroy(element);
        }

        protected override void ModifyPutObject(GameObject element)
        {
            element.SetActive(false);
        }

        protected override void ModifyGetObject(GameObject element)
        {
            element.SetActive(true);
        }
    }
}