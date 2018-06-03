using System.Threading;

namespace Client
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