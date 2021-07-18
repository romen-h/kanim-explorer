using System;
using System.Diagnostics;

using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.OpenGL.Objects
{
	/// <summary>
	/// Represents a managed OpenGL vertex array.
	/// </summary>
	public class VAO
	{
		PrimitiveType type;
		int id;
		VertexBuffer vbo;
		ElementBuffer ebo;

		public int ID => id;

		public VAO()
		{
			type = PrimitiveType.Points;
			id = 0;
			vbo = new VertexBuffer();
			ebo = new ElementBuffer();
		}

		~VAO()
		{
			if (id > 0) Trace.WriteLine("XOpenGL: VAO leak.");
			Release();
		}

		private void AssertAllocated()
		{
			if (id == 0) throw new InvalidOperationException("The VAO has not been allocated yet.");
		}

		public void Allocate()
		{
			if (id > 0) return;
			id = GL.GenVertexArray();

			vbo.Allocate();
			ebo.Allocate();
		}

		public void Release()
		{
			if (id > 0)
			{
				GL.DeleteVertexArray(id);
				id = 0;
			}

			vbo.Release();
			ebo.Release();
		}

		public void Bind()
		{
			AssertAllocated();
			GL.BindVertexArray(id);
		}

		public void SetPrimitiveType(PrimitiveType t)
		{
			switch (t)
			{
				case PrimitiveType.Points:
				case PrimitiveType.LineStrip:
				case PrimitiveType.LineLoop:
				case PrimitiveType.Lines:
				case PrimitiveType.Triangles:
				case PrimitiveType.TriangleStrip:
				case PrimitiveType.TriangleFan:
					break;

				default:
					throw new ArgumentException("Unsupported primitive type.");
			}

			type = t;
		}

		public void SetVertices(Vertex[] vertices, bool dynamic = true)
		{
			Bind();
			vbo.SetVertices(vertices, dynamic);

			// Position Attribute
			GL.VertexAttribPointer(0, Vertex.PositionSize, VertexAttribPointerType.Float, false, Vertex.Size, (IntPtr)Vertex.PositionOffset);
			GL.EnableVertexAttribArray(0);

			// Normal Attribute
			GL.VertexAttribPointer(1, Vertex.NormalSize, VertexAttribPointerType.Float, true, Vertex.Size, (IntPtr)Vertex.NormalOffset);
			GL.EnableVertexAttribArray(1);

			// Color Attribute
			GL.VertexAttribPointer(2, Vertex.ColorSize, VertexAttribPointerType.Float, false, Vertex.Size, (IntPtr)Vertex.ColorOffset);
			GL.EnableVertexAttribArray(2);

			// TexCoord Attribute
			GL.VertexAttribPointer(3, Vertex.TexCoordSize, VertexAttribPointerType.Float, false, Vertex.Size, (IntPtr)Vertex.TexCoordOffset);
			GL.EnableVertexAttribArray(3);
		}

		public void SetElements(int[] elements, bool dynamic = true)
		{
			Bind();
			ebo.SetElements(elements, dynamic);
		}

		public void SetData(PrimitiveType t, Vertex[] vertices, int[] elements, bool dynamic = true)
		{
			Bind();
			SetPrimitiveType(t);
			SetVertices(vertices, dynamic);
			SetElements(elements, dynamic);
		}

		public void Draw()
		{
			if (ebo.ElementCount == 0) return;

			Bind();
			GL.DrawElements(type, ebo.ElementCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}
	}
}
