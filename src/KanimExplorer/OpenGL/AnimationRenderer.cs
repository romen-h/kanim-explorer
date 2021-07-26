using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanimExplorer.OpenGL.Objects;
using KanimExplorer.OpenGL.Shaders;

using KanimLib;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace KanimExplorer.OpenGL
{
	public class AnimationRenderer
	{
		private Shader geoShader;
		private Shader spriteShader;
		private Texture texture;

		private Matrix4 viewMat;
		private Matrix4 projMat;

		private VAO origin;
		private VAO quad;

		public AnimationRenderer()
		{
			geoShader = new Shader();
			spriteShader = new Shader();
			texture = new Texture();
			origin = new VAO();
			quad = new VAO();
		}

		public void Initialize()
		{
			GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Multisample);

			geoShader.Initialize(VertexShaders.MVPTransformed, FragmentShaders.SolidColor);
			spriteShader.Initialize(VertexShaders.MVPTransformed, FragmentShaders.Textured);
			
			texture.Allocate();

			origin.Allocate();
			origin.SetData(PrimitiveType.Lines,
				new Vertex[]
				{
					new Vertex(-10000, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0),
					new Vertex( 10000, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0),
					new Vertex(0, -10000, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0),
					new Vertex(0,  10000, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0)
				},
				new int[] { 0, 1, 2, 3 },
				false
			);

			quad.Allocate();
		}

		public void Release()
		{
			geoShader.Release();
			geoShader = null;

			spriteShader.Release();
			spriteShader = null;

			texture.Release();
			texture = null;

			origin.Release();
			origin = null;

			quad.Release();
			quad = null;
		}

		public void SetViewport(int width, int height)
		{
			float margin = 200f;
			projMat = Matrix4.CreateOrthographicOffCenter(-width, width, -margin, 2*height-margin, 10f, -10f);
			viewMat = Matrix4.LookAt(new Vector3(0, 0, 10f), Vector3.Zero, new Vector3(0, 1f, 0));

			geoShader.SetUniform("Projection", ref projMat);
			geoShader.SetUniform("View", ref viewMat);
			spriteShader.SetUniform("Projection", ref projMat);
			spriteShader.SetUniform("View", ref viewMat);
		}

		public void SetTexture(Bitmap bmp)
		{
			if (texture == null) throw new InvalidOperationException("AnimationRender has not been initialized for OpenGL yet.");

			try
			{
				Bitmap clone = new Bitmap(bmp);
				System.Drawing.Imaging.BitmapData bmpData = clone.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

				texture.SetTextureData(bmpData);

				clone.UnlockBits(bmpData);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		Matrix4 halfScaleMat = Matrix4.CreateScale(0.5f);
		Matrix4 dblScaleMat = Matrix4.CreateScale(2f);

		public void Render(KBuild build, KAnimBank bank, int frameNumber)
		{
			Matrix4 modelMat = Matrix4.Identity;
			Matrix4 pivotMat = Matrix4.Identity;
			Matrix4 transformMat = Matrix4.Identity;


			geoShader.Activate();
			// Draw Origin
			geoShader.SetUniform("Model", ref modelMat);
			origin.Draw();

			if (frameNumber < 0 || frameNumber >= bank.FrameCount) frameNumber = 0;

			KAnimFrame frame = bank.Frames[frameNumber];

			Matrix4 frameMat = Matrix4.CreateTranslation(frame.X, frame.Y, 0);

			spriteShader.Activate();
			texture.Bind();

			for (int i=frame.ElementCount-1; i >= 0; i--)
			{
				KAnimElement element = frame.Elements[i];
				string symbol = build.GetSymbolName(element.SymbolHash);
				if (symbol == null) continue;
				KFrame sprite = build.GetFrame(symbol, element.FrameNumber);
				if (sprite == null) continue;

				pivotMat = Matrix4.CreateTranslation(sprite.PivotX, sprite.PivotY, 0);

				transformMat = new Matrix4(
					element.M00, -element.M10, 0, 0,
					element.M01, -element.M11, 0, 0,
					0, 0, -1, 0,
					element.M02, -element.M12, 0, 1
				);

				modelMat = halfScaleMat * pivotMat * transformMat;

				spriteShader.SetUniform("Model", ref modelMat);

				MakeQuadVBO(sprite.PivotWidth, sprite.PivotHeight, element.Red, element.Green, element.Blue, element.Alpha, sprite.GetUVRectangle(), out Vertex[] vertices, out int[] elements);
				quad.SetData(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, vertices, elements);

				quad.Draw();
			}
		}

		private void MakeQuadVBO(float halfWidth, float halfHeight, float r, float g, float b, float a, RectangleF uv, out Vertex[] vertices, out int[] elements)
		{
			vertices = new Vertex[]{
						   // positions					//normal    // colors       // uv
				new Vertex( halfWidth,  halfHeight, 0f, 0f, 0f, 1f, r, g, b, a, uv.Right, uv.Bottom),   // top right
				new Vertex( halfWidth, -halfHeight, 0f, 0f, 0f, 1f, r, g, b, a, uv.Right, uv.Top),   // bottom right
				new Vertex(-halfWidth, -halfHeight, 0f, 0f, 0f, 1f, r, g, b, a, uv.Left, uv.Top),   // bottom left
				new Vertex(-halfWidth,  halfHeight, 0f, 0f, 0f, 1f, r, g, b, a, uv.Left, uv.Bottom)    // top left 
			};

			elements = new int[] { 0, 1, 3, 1, 2, 3 };
		}
	}
}
