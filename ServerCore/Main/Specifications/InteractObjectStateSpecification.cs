using System.Collections.Generic;
using ServerCore.Main.Specifications.Base;
using ServerCore.Main.Utilities.SimpleJson;

namespace ServerCore.Main.Specifications
{
    public class InteractObjectStateSpecification : Specification
    {
        public int StateCount;
    
        public override void Fill(IDictionary<string, object> node)
        {
            _id = node.GetString("id");
            StateCount = node.GetInt("state_count");
        }
    }
}