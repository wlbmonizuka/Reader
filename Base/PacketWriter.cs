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
                        var bufferClient0 = new byte[] {
                                0x1b,
                                0x0c, 0x00, 0x00, 0x00
                            };

                        writer.WriteTag(Tag.Client);
                        writer.WritePacket(bufferClient0);

                        Utils.Log(ConsoleColor.Yellow, Utils.ToHexStr(bufferClient0));
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        var bufferClient1 = new byte[] {
                                0x38,
                                0x31, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00
                            };

                        writer.WriteTag(Tag.Client);
                        writer.WritePacket(bufferClient1);

                        Utils.Log(ConsoleColor.Yellow, Utils.ToHexStr(bufferClient1));
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        var bufferServer = new byte[] {
                                0x4A, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x73, 0x00, 0x2E, 0x00, 0x6C,
                                0x00, 0x69, 0x00, 0x6E, 0x00, 0x32, 0x00, 0x63, 0x00, 0x2E, 0x00, 0x72, 0x00, 0x75, 0x00, 0x00,
                                0x00
                            };

                        writer.WriteTag(Tag.Server);
                        writer.WritePacket(bufferServer);

                        Utils.Log(ConsoleColor.Cyan, Utils.ToHexStr(bufferServer));
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        writer.WriteTag(Tag.Debug);

                        Utils.Log(ConsoleColor.Magenta, $"{nameof(Tag)}.{Tag.Debug}");
                        break;
                }
            }
        }
    }
}