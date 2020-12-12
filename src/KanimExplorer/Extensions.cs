using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace KanimalExplorer
{
	public static class BinaryReaderExtensions
	{
        public static string ReadKString(this BinaryReader reader)
        {
            var i = reader.ReadInt32();
            if (i <= 0) return "";
            var buff = reader.ReadBytes(i);
            return System.Text.Encoding.ASCII.GetString(buff);
        }

        public static Color ReadColor32(this BinaryReader reader)
		{
            int i = reader.ReadInt32();
            return Color.FromArgb(i);
		}

        public static KSymbol.SymbolFlags ReadKSymbolFlags(this BinaryReader reader)
		{
            int i = reader.ReadInt32();
            return (KSymbol.SymbolFlags)i;
		}
    }

    public static class BinaryWriterExtensions
	{
        public static void WriteKString(this BinaryWriter writer, string str)
		{
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            writer.Write(bytes.Length);
            writer.Write(bytes);
		}

        public static void Write(this BinaryWriter writer, Color color)
		{
            int i = color.ToArgb();
            writer.Write(i);
		}

        public static void Write(this BinaryWriter writer, KSymbol.SymbolFlags flags)
		{
            int i = (int)flags;
            writer.Write(i);
		}
	}

    public static class SymbolFlagsExtensions
    {
        public static KSymbol.SymbolFlags SetFlag(this KSymbol.SymbolFlags flags, KSymbol.SymbolFlags flag, bool value)
        {
            int flagsInt = (int)flags;
            int flagInt = (int)flag;
            if (value)
            {
                flagsInt |= flagInt;
            }
            else
            {
                flagsInt &= ~flagInt;
            }
            return (KSymbol.SymbolFlags)flagsInt;
        }
    }
}
