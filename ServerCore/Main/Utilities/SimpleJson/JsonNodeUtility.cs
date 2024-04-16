using System;
using System.Collections.Generic;

namespace ServerCore.Main.Utilities.SimpleJson
{
    public static class JsonNodeUtility
    {
        public static string GetString(this IDictionary<string, object> node, string key) => Convert.ToString(node[key]);
        public static int GetInt(this IDictionary<string, object> node, string key) => Convert.ToInt32(node[key]);
        public static float GetFloat(this IDictionary<string, object> node, string key) => (float)Convert.ToDouble(node[key]);
        public static bool GetBoolean(this IDictionary<string, object> node, string key) => Convert.ToBoolean(node[key]);
        public static List<IDictionary<string, object>> GetNodes(this IDictionary<string, object> node, string key)
        {
            List<IDictionary<string, object>> result = new();
            
            foreach (var element in (object[])node[key])
            {
                result.Add((IDictionary<string, object>)element);
            }

            return result;
        }
    }
}