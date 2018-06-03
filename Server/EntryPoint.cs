using System.Threading;

namespace Server
{
    class EntryPoint
    {
        static void Main()
        {
            new Core().Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}