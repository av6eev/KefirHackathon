namespace ServerCore.Main.Utilities.Logger
{
    public class Logger
    {
        private static ILogger _instance;
    
        public static ILogger Instance => _instance;

        public static void SetLogger(ILogger logger)
        {
            _instance = logger;
        }
    }
}