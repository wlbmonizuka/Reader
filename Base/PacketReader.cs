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
                    var buffer = reader.ReadBytes(2);
                    var size = BitConverter.ToUInt16(buffer, 0);

                    buffer = reader.ReadBytes(size - 2);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{nameof(PacketReader)}: {Utils.ToHexStr(buffer)}");
                }
        }
    }
}