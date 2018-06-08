using System;
using System.IO;

namespace Base
{
    public class PacketReader
    {
        BinaryReader reader;
        public PacketReader(BinaryReader reader) =>
            this.reader = reader;

        public event Action<Tag, byte[]> OnRead;

        public void Start()
        {
            while (true)
            {
                var tag = reader.ReadTag();
                switch (tag)
                {
                    case Tag.Client:
                        var pckClient = reader.ReadPacket();
                        Utils.Log(ConsoleColor.DarkYellow, Utils.ToHexStr(pckClient));
                        OnRead?.Invoke(tag, pckClient);
                        break;

                    case Tag.Server:
                        var pckServer = reader.ReadPacket();
                        Utils.Log(ConsoleColor.DarkCyan, Utils.ToHexStr(pckServer));
                        OnRead?.Invoke(tag, pckServer);
                        break;

                    case Tag.Debug:
                        Utils.Log(ConsoleColor.DarkMagenta, "Debug message");
                        break;

                    default:
                        Utils.Log(ConsoleColor.DarkRed, "Unknown message");
                        break;
                }
            }
        }
    }
}