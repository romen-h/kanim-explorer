using OpenTK;
using OpenTK.Graphics;

namespace KanimExplorer.OpenGL.Objects
{
	/// <summary>
	/// A standard 3D vertex with attributes.
	/// </summary>
	public struct Vertex
	{
		public const int PositionOffset = 0;
		public const int PositionSize = 3;
		public const int NormalOffset = PositionOffset + PositionSize * sizeof(float);
		public const int NormalSize = 3;
		public const int ColorOffset = NormalOffset + NormalSize * sizeof(float);
		public const int ColorSize = 4;
		public const int TexCoordOffset = ColorOffset + ColorSize * sizeof(float);
		public const int TexCoordSize = 2;
		public const int Size = 12 * sizeof(float);

		public float X;
		public float Y;
		public float Z;

		public float NX;
		public float NY;
		public float NZ;

		public float R;
		public float G;
		public float B;
		public float A;

		public float U;
		public float V;

		public Vector3 Position => new Vector3(X, Y, Z);
		public Vector3 Normal => new Vector3(NX, NY, NZ);
		public Color4 Color => new Color4(R, G, B, A);
		public Vector2 TexCoords => new Vector2(U, V);

		public Vertex(float x, float y, float z, float nx, float ny, float nz, float r, float g, float b, float a, float u, float v)
		{
			X = x; Y = y; Z = z;
			NX = nx; NY = ny; NZ = nz;
			R = r; G = g; B = b; A = a;
			U = u; V = v;
		}
	}
}
