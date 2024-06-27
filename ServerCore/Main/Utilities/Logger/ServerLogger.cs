using System;

namespace ServerCore.Main.Utilities.Logger
{
    public class ServerLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}