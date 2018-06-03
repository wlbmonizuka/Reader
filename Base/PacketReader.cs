using System;
using System.IO;
using System.Net.Sockets;

namespace Base
{
    public class PacketReader
    {
        Socket socket;
        public PacketReader(Socket socket) =>
            this.socket = socket;

        public static void StartNew(Socket socket) =>
            new PacketReader(socket).Start();

        public void Start()
        {
            using (var stream = new NetworkStream(socket))
            using (var reader = new BinaryReader(stream))
                while (true)
                {
                    var tag = reader.ReadTag();
                    switch (tag)
                    {
                        case Tag.Client:
                            var pckClient = reader.ReadPacket();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(Utils.ToHexStr(pckClient));
                            break;

                        case Tag.Server:
                            var pckServer = reader.ReadPacket();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(Utils.ToHexStr(pckServer));
                            break;

                        case Tag.Debug:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Debug message");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Unknown message");
                            break;
                    }
                }
        }
    }
}