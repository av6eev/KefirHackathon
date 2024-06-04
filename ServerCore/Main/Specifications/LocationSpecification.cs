using System.Collections.Generic;
using ServerCore.Main.Specifications.Base;
using ServerCore.Main.Utilities.SimpleJson;

namespace ServerCore.Main.Specifications
{
    public class LocationSpecification : Specification
    {
        public readonly Dictionary<string, int> InteractObjects = new();
    
        public override void Fill(IDictionary<string, object> node)
        {
            _id = node.GetString("id");
            
            foreach (var element in node.GetNodes("interact_objects"))
            {
                InteractObjects.Add(element.GetString("id"), element.GetInt("count")); 
            }
        }
    }
}