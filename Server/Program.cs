namespace Server
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new Server().Start();

            while (true) {}
        }
    }
}