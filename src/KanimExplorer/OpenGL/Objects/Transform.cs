using System;

using OpenTK;
using OpenTK.Mathematics;

namespace KanimExplorer.OpenGL.Objects
{
	/// <summary>
	/// Represents a 3D transformation with position, rotation, and scale.
	/// </summary>
	public class Transform
	{
		public event EventHandler Changed;

		/// <summary>
		/// Gets or sets the transformation matrix.
		/// </summary>
		public Matrix4 Matrix
		{
			get { return Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rot) * Matrix4.CreateTranslation(pos); }
			set
			{
				pos = value.ExtractTranslation();
				rot = value.ExtractRotation();
				scale = value.ExtractScale();
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		private Vector3 pos;
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Vector3 Position
		{
			get { return pos; }
			set
			{
				pos = value;
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		private Quaternion rot;
		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		public Quaternion Rotation
		{
			get { return rot; }
			set
			{
				rot = value;
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		private Vector3 scale;
		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		public Vector3 Scale
		{
			get { return scale; }
			set
			{
				scale = value;
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		public Transform()
		{
			pos = Vector3.Zero;
			rot = Quaternion.Identity;
			scale = Vector3.One;
		}
	}
}
