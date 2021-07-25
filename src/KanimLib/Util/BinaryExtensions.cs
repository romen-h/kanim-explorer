using System.Drawing;
using System.IO;
using System.Text;

namespace KanimLib
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

		public static SymbolFlags ReadKSymbolFlags(this BinaryReader reader)
		{
			int i = reader.ReadInt32();
			return (SymbolFlags)i;
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

		public static void Write(this BinaryWriter writer, SymbolFlags flags)
		{
			int i = (int)flags;
			writer.Write(i);
		}
	}
}
