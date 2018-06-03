using System;
using System.IO;
using System.Net.Sockets;

namespace Base
{
    public class PacketWriter
    {
        Socket socket;
        public PacketWriter(Socket socket) =>
            this.socket = socket;

        public static void StartNew(Socket socket) =>
            new PacketWriter(socket).Start();

        public void Start()
        {
            using (var stream = new NetworkStream(socket))
            using (var writer = new BinaryWriter(stream))
                while (true)
                {
                    //По нажатию клавиши в консоли инжектим пакет
                    Console.ReadKey();
                    var buffer = new byte[]
                    {
                        0x0b, 0x00,      //размер    -   11 Байт, надеюсь не перепутал порядок байтов) поправишь
                        0x38,
                        0x31, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    };

                    writer.Write(buffer);
                    //writer.Flush();     //это чтобы собранный пакет ушел в сеть

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"{nameof(PacketWriter)}: {Utils.ToHexStr(buffer)}");
                }
        }
    }
}