namespace KanimExplorer.OpenGL.Shaders
{
	public static class VertexShaders
	{
		public const string NoTransform =
@"#version 330 core
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec4 color;
layout(location = 3) in vec2 texcoord;

out vec3 Normal;
out vec4 Color;
out vec2 TexCoord;

void main()
{
	gl_Position = vec4(position, 1.0);
	Normal = normal;
	Color = color;
	TexCoord = texcoord;
}";

		public const string MatrixTransformed =
@"#version 330 core
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec4 color;
layout(location = 3) in vec2 texcoord;

out vec3 Normal;
out vec4 Color;
out vec2 TexCoord;

uniform mat4 Transform;

void main()
{
	gl_Position = Transform * vec4(position, 1.0);
	Normal = normal;
	Color = color;
	TexCoord = texcoord;
}";

		public const string MVPTransformed =
@"#version 330 core
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec4 color;
layout(location = 3) in vec2 texcoord;

out vec3 Normal;
out vec4 Color;
out vec2 TexCoord;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

void main()
{
	gl_Position = Projection * View * Model * vec4(position, 1.0);
	Normal = normal;
	Color = color;
	TexCoord = texcoord;
}";

	}
}
