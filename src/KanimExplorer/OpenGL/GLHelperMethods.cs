using System;
using System.Diagnostics;
using System.Drawing;

using KanimExplorer.OpenGL.Objects;

using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.OpenGL
{
	internal static class GLHelperMethods
	{
		[Conditional("DEBUG")]
		internal static void CheckError()
		{
			ErrorCode err = GL.GetError();
			if (err != ErrorCode.NoError)
			{
				throw new Exception($"OpenGL Error: {err}");
			}
		}

		/// <summary>
		/// Returns the OpenGL PixelFormat for the given channels.
		/// </summary>
		/// <param name="channels">The number of color channels in the pixel.</param>
		/// <param name="bgr">Whether the color channels are in BGR order.</param>
		internal static PixelFormat ResolvePixelFormat(int channels, bool bgr = false)
		{
			switch (channels)
			{
				case 1: return PixelFormat.Red;
				case 3: return bgr ? PixelFormat.Bgr : PixelFormat.Rgb;
				case 4: return bgr ? PixelFormat.Bgra : PixelFormat.Rgba;
				default: throw new ArgumentException("Unsupported channel count.");
			}
		}

		/// <summary>
		/// Returns the OpenGL PixelType for the given bit-depth.
		/// </summary>
		/// <param name="depth">The bit-depth of the pixel.</param>
		/// <param name="floating">Whether the pixel is a floating point value.</param>
		internal static PixelType ResolvePixelType(int depth, bool floating = false)
		{
			if (floating)
			{
				switch (depth)
				{
					case 16: return PixelType.HalfFloat;
					case 32: return PixelType.Float;
					default: throw new ArgumentException("Unsupported bit-depth for floating point pixels.");
				}
			}
			else
			{
				switch (depth)
				{
					case 8: return PixelType.UnsignedByte;
					case 16: return PixelType.UnsignedShort;
					default: throw new ArgumentException("Unsupported bit-depth for integer pixels.");
				}
			}
		}
	}
}
