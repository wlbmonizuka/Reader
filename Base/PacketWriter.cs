using System;
using System.IO;

namespace Base
{
    public class PacketWriter
    {
        BinaryWriter writer;
        public PacketWriter(BinaryWriter writer) =>
            this.writer = writer;

        public void Start()
        {
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

                        Utils.Log(ConsoleColor.Yellow, Utils.ToHexStr(bufferClient));
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

                        Utils.Log(ConsoleColor.Cyan, Utils.ToHexStr(bufferServer));
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        writer.WriteTag(Tag.Debug);

                        Utils.Log(ConsoleColor.Magenta, $"{nameof(Tag)}.{Tag.Debug}");
                        break;
                }
            }
        }
    }
}