using System;
using System.Diagnostics;

using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.OpenGL.Objects
{
	/// <summary>
	/// Represents a managed OpenGL texture.
	/// </summary>
	public class Texture
	{
		protected int id;
		protected int width;
		protected int height;
		protected int numChannels;
		protected int bitDepth;

		public bool IsAllocated => (id > 0);

		public int ID => id;
		public int Width => width;
		public int Height => height;

		public Texture()
		{
			id = 0;
			width = 0;
			height = 0;
			numChannels = 0;
			bitDepth = 0;
		}

		~Texture()
		{
			if (IsAllocated) Trace.WriteLine("XOpenGL: Texture leak.");
		}

		private void AssertAllocated()
		{
			if (id == 0) throw new InvalidOperationException("The texture has not been allocated yet.");
		}

		public virtual void Allocate()
		{
			if (id > 0) return;

			id = GL.GenTexture();
		}

		public virtual void Release()
		{
			if (id > 0)
			{
				GL.DeleteTexture(id);
				id = 0;
			}
		}

		public void Bind(TextureUnit texUnit = TextureUnit.Texture0)
		{
			AssertAllocated();
			GL.ActiveTexture(texUnit);
			GL.BindTexture(TextureTarget.Texture2D, id);
		}

		public virtual void SetTextureData(int w, int h, int channels, int depth, bool bgr, IntPtr data)
		{
			Bind();

			PixelFormat fmt = GLHelperMethods.ResolvePixelFormat(channels, bgr);
			PixelType pt = GLHelperMethods.ResolvePixelType(depth);

			bool resized = (width != w || height != h || numChannels != channels || bitDepth != depth);
			width = w;
			height = h;
			numChannels = channels;
			bitDepth = depth;

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
			// TODO: Smooth?
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			// Upload
			if (resized)
			{
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, fmt, pt, data);
			}
			else
			{
				GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, fmt, pt, data);
			}
		}

		public void SetTextureData(System.Drawing.Imaging.BitmapData bmpData)
		{
			int channels = 0;
			switch (bmpData.PixelFormat)
			{
				case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
					channels = 4;
					break;

				case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
					channels = 3;
					break;

				default:
					throw new NotImplementedException("Unsupported pixel format.");
			}

			SetTextureData(bmpData.Width, bmpData.Height, channels, 8, true, bmpData.Scan0);
		}
	}

	/// <summary>
	/// Represents a managed OpenGL texture with a PBO for frequent transfers.
	/// </summary>
	public class HotTexture : Texture
	{
		PixelBuffer pbo;

		public HotTexture() : base()
		{
			pbo = new PixelBuffer();
		}

		public override void Allocate()
		{
			base.Allocate();

			pbo.Allocate();
		}

		public override void Release()
		{
			base.Release();

			pbo.Release();
		}

		public override void SetTextureData(int w, int h, int channels, int depth, bool bgr, IntPtr data)
		{
			Bind();

			pbo.Bind();
			int pboSize = w * h * channels * (depth / 8);

			// If we allocate the buffer once with a null pointer and then again with the data
			// we can take advantage of the graphics driver implicitly handling the buffers as a round robin.
			// This optimization appears to be more efficient on AMD GPUs but works for most graphics drivers.
			pbo.UploadData(IntPtr.Zero, pboSize, BufferUsageHint.StreamDraw);
			pbo.UploadData(data, pboSize, BufferUsageHint.StreamDraw);

			base.SetTextureData(w, h, channels, depth, bgr, IntPtr.Zero);
		}
	}
}
