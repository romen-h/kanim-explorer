using System;

using OpenTK;

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

		private OpenTK.Vector3 pos;
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public OpenTK.Vector3 Position
		{
			get { return pos; }
			set
			{
				pos = value;
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		private OpenTK.Quaternion rot;
		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		public OpenTK.Quaternion Rotation
		{
			get { return rot; }
			set
			{
				rot = value;
				Changed?.Invoke(this, EventArgs.Empty);
			}
		}

		private OpenTK.Vector3 scale;
		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		public OpenTK.Vector3 Scale
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
			pos = OpenTK.Vector3.Zero;
			rot = OpenTK.Quaternion.Identity;
			scale = OpenTK.Vector3.One;
		}
	}
}
