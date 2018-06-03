using System;
using System.IO;

namespace Base
{
    public static class BinaryReaderExtensions
    {
        public static Tag ReadTag(this BinaryReader reader) =>
            (Tag)reader.ReadByte();

        public static UInt16 ReadSize(this BinaryReader reader)
        {
            var buffer = reader.ReadBytes(2);
            var size = BitConverter.ToUInt16(buffer, 0);

            return size;
        }

        public static byte[] ReadPacket(this BinaryReader reader)
        {
            var size = reader.ReadSize();
            var buffer = reader.ReadBytes(size - 2);

            return buffer;
        }
    }
}