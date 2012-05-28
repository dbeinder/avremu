using System;
namespace avrEmu
{
    public static class WordHelper
    {
        public static ExtByte[] ToBytes(ushort word)
        {
            return new ExtByte[] { GetLowByte(word), GetHighByte(word) };
        }

        public static ExtByte GetLowByte(ushort word)
        {
            return new ExtByte((byte)(word & 0x00ff));
        }

        public static ExtByte GetHighByte(ushort word)
        {
            return new ExtByte((byte)((word & 0xff00) >> 8));
        }

        public static ushort FromBytes (ExtByte low, ExtByte high)
		{
			return (ushort)((ushort)low.Value + ((ushort)high.Value << 8));
        }

        public static ushort FromBytes (ExtByte[] bytes)
		{
			return (ushort)((ushort)bytes [0].Value + ((ushort)bytes[1].Value << 8));
        }
    }
}

