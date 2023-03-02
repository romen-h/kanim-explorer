using System;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace KanimExplorer.OpenGL.Objects
{
	public class Shader
	{
		private int id;

		public bool IsInitialized => id > 0;

		public Shader()
		{
			id = 0;
		}

		~Shader()
		{
			if (id > 0) Trace.WriteLine("XOpenGL: Shader object leak.");
		}

		public void AssertInitialized()
		{
			if (id == 0) throw new InvalidOperationException("The shader has not been initialized yet.");
		}

		public void Initialize(string vertCode, string fragCode)
		{
			int vsID = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vsID, vertCode);
			GL.CompileShader(vsID);
			if (!ValidateShader(vsID, "vertex"))
			{
				GL.DeleteShader(vsID);
				throw new Exception("Failed to compile vertex shader.");
			}

			int fsID = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fsID, fragCode);
			GL.CompileShader(fsID);
			if (!ValidateShader(fsID, "fragment"))
			{
				GL.DeleteShader(vsID);
				GL.DeleteShader(fsID);
				throw new Exception("Failed to compile fragment shader.");
			}

			id = GL.CreateProgram();
			GL.AttachShader(id, vsID);
			GL.AttachShader(id, fsID);
			GL.LinkProgram(id);
			if (!ValidateProgram())
			{
				GL.DeleteShader(vsID);
				GL.DeleteShader(fsID);
				throw new Exception("Failed to link shader program.");
			}

			GL.DeleteShader(vsID);
			GL.DeleteShader(fsID);

#if GL_TRACE
			Trace.WriteLine("[GL DEBUG] Shader successfully compiled & linked.");
#endif
		}

		public void Release()
		{
			if (id > 0)
			{
				GL.DeleteProgram(id);
				id = 0;
			}
		}

		public void Activate()
		{
			AssertInitialized();
			GL.UseProgram(id);
		}

		private int Location(string name)
		{
			return GL.GetUniformLocation(id, name);
		}

		public bool SupportsUniform(string name)
		{
			return (Location(name) >= 0);
		}

		public void SetUniform(string name, int value)
		{
			Activate();
			GL.Uniform1(Location(name), value);
		}

		public void SetUniform(string name, float value)
		{
			Activate();
			GL.Uniform1(Location(name), value);
		}

		public void SetUniform(string name, float x, float y)
		{
			Activate();
			GL.Uniform2(Location(name), x, y);
		}

		public void SetUniform(string name, ref Vector2 vec)
		{
			Activate();
			GL.Uniform2(Location(name), vec);
		}

		public void SetUniform(string name, float x, float y, float z)
		{
			Activate();
			GL.Uniform3(Location(name), x, y, z);
		}

		public void SetUniform(string name, ref Vector3 vec)
		{
			Activate();
			GL.Uniform3(Location(name), vec);
		}

		public void SetUniform(string name, ref Color4 color)
		{
			Activate();
			GL.Uniform4(Location(name), color);
		}

		public void SetUniform(string name, ref Matrix4 mat)
		{
			Activate();
			GL.UniformMatrix4(Location(name), false, ref mat);
		}

		private bool ValidateShader(int shaderID, string name)
		{
			GL.GetShader(shaderID, ShaderParameter.CompileStatus, out int success);
			if (success == 0)
			{
#if GL_TRACE
				string info;
				GL.GetShaderInfoLog(shaderID, out info);
				Trace.WriteLine($"[GL ERROR] Failed to compile {name} shader.\n{info}");
#endif
				return false;
			}

			return true;
		}

		private bool ValidateProgram()
		{
			GL.GetProgram(id, GetProgramParameterName.LinkStatus, out int success);
			if (success == 0)
			{
#if GL_TRACE
				string info;
				GL.GetProgramInfoLog(id, out info);
				Trace.WriteLine($"[GL ERROR] Failed to link shader program.\n{info}");
#endif
				return false;
			}

			return true;
		}
	}
}
