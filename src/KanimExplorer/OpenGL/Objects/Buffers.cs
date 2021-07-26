using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.OpenGL.Objects
{
	/// <summary>
	/// An abstraction of a managed OpenGL buffer object.
	/// </summary>
	public abstract class BufferObject
	{
		int id = 0;

		public bool IsAllocated => (id > 0);
		public int ID => id;

		protected abstract BufferTarget BufferType
		{ get; }

		~BufferObject()
		{
			if (IsAllocated) Trace.WriteLine("XOpenGL: BufferObject leak.");
		}

		private void AssertAllocated()
		{
			if (id == 0) throw new InvalidOperationException("The buffer object has not been allocated yet.");
		}

		/// <summary>
		/// Allocates memory for the buffer.
		/// </summary>
		public virtual void Allocate()
		{
			if (id > 0) return;
			id = GL.GenBuffer();
		}

		/// <summary>
		/// Releases the memory used for the buffer.
		/// </summary>
		public virtual void Release()
		{
			if (id > 0)
			{
				GL.DeleteBuffer(id);
				id = 0;
			}
		}

		/// <summary>
		/// Binds the buffer for use by OpenGL.
		/// </summary>
		public void Bind()
		{
			AssertAllocated();
			GL.BindBuffer(BufferType, id);
		}

		/// <summary>
		/// Uploads data to the buffer on the GPU.
		/// </summary>
		/// <param name="data">A pointer to the data.</param>
		/// <param name="size">The size of the data.</param>
		/// <param name="hint">A hint for how the buffer data will be used.</param>
		public virtual void UploadData(IntPtr data, int size, BufferUsageHint hint)
		{
			Bind();
			GL.BufferData(BufferType, size, data, hint);
		}
	}

	/// <summary>
	/// Represents a managed OpenGL pixel buffer.
	/// </summary>
	public class PixelBuffer : BufferObject
	{
		private bool unpackMode;

		protected override BufferTarget BufferType => (unpackMode ? BufferTarget.PixelUnpackBuffer : BufferTarget.PixelPackBuffer);

		public PixelBuffer(bool unpack = true)
		{
			unpackMode = unpack;
		}
	}

	/// <summary>
	/// Represents a managed OpenGL vertex buffer.
	/// </summary>
	public class VertexBuffer : BufferObject
	{
		protected override BufferTarget BufferType => BufferTarget.ArrayBuffer;

		~VertexBuffer()
		{
			Release();
		}

		/// <summary>
		/// Uploads the given vertex array to the vbo.
		/// </summary>
		public void SetVertices(Vertex[] vertices, bool dynamic = true)
		{
			if (vertices == null || vertices.Length == 0) throw new ArgumentException("Vertex list is null or empty.");
			Bind();

			// Upload vertex data
			int vboSize = vertices.Length * Vertex.Size;
			GCHandle vboHandle = GCHandle.Alloc(vertices, GCHandleType.Pinned);
			try
			{
				UploadData(vboHandle.AddrOfPinnedObject(), vboSize, dynamic ? BufferUsageHint.DynamicDraw : BufferUsageHint.StaticDraw);
			}
			finally
			{
				vboHandle.Free();
			}
		}
	}

	/// <summary>
	/// Represents a managed OpenGL element buffer.
	/// </summary>
	public class ElementBuffer : BufferObject
	{
		private int elementCount = 0;

		public int ElementCount => elementCount;

		protected override BufferTarget BufferType => BufferTarget.ElementArrayBuffer;

		~ElementBuffer()
		{
			Release();
		}

		public void SetElements(int[] elements, bool dynamic = true)
		{
			if (elements == null || elements.Length == 0) throw new ArgumentException("Element list is null or empty.");
			Bind();

			// Upload element data
			int eboSize = elements.Length * sizeof(int);
			var eboHandle = GCHandle.Alloc(elements, GCHandleType.Pinned);
			try
			{
				UploadData(eboHandle.AddrOfPinnedObject(), eboSize, dynamic ? BufferUsageHint.DynamicDraw : BufferUsageHint.StaticDraw);
			}
			finally
			{
				eboHandle.Free();
			}
			elementCount = elements.Length;
		}
	}
}
