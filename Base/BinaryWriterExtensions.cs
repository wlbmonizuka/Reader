using System;
using System.IO;

namespace Base
{
    public static class BinaryWriterExtensions
    {
        public static void WriteTag(this BinaryWriter writer, Tag tag) =>
            writer.Write((byte)tag);

        public static void WriteSize(this BinaryWriter writer, UInt16 size)
        {
            var bytes = BitConverter.GetBytes(size);
            writer.Write(bytes);
        }

        public static void WritePacket(this BinaryWriter writer, byte[] buffer)
        {
            writer.WriteSize((UInt16)(buffer.Length + 2));
            writer.Write(buffer);
        }
    }
}