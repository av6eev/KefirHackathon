using UnityEngine;
using ILogger = ServerCore.Main.Utilities.Logger.ILogger;

namespace Utilities.Logger
{
    public class UnityLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.Log(message);            
        }
    }
}