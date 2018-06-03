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
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            var bufferClient = new byte[] {
                                0x38,
                                0x31, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00
                            };

                            writer.WriteTag(Tag.Client);
                            writer.WritePacket(bufferClient);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(Utils.ToHexStr(bufferClient));
                            break;

                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            var bufferServer = new byte[] {
                                0x38,
                                0x31, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00
                            };

                            writer.WriteTag(Tag.Server);
                            writer.WritePacket(bufferServer);

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(Utils.ToHexStr(bufferServer));
                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            writer.WriteTag(Tag.Debug);

                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"{nameof(Tag)}.{Tag.Debug}");
                            break;
                    }
                }
        }
    }
}