namespace KanimExplorer.OpenGL.Shaders
{
	public static class FragmentShaders
	{
		public const string Black =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = vec4(0.0, 0.0, 0.0, 1.0);
}";

		public const string Red =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = vec4(1.0, 0.0, 0.0, 1.0);
}";

		public const string Green =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = vec4(0.0, 1.0, 0.0, 1.0);
}";

		public const string Blue =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = vec4(0.0, 0.0, 1.0, 1.0);
}";

		public const string White =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = vec4(1.0, 1.0, 1.0, 1.0);
}";

		public const string SolidColor =
@"#version 330 core
out vec4 FragColor;

in vec4 Color;
in vec3 Normal;
in vec2 TexCoord;

void main()
{
	FragColor = Color;
}";

		public const string Textured =
@"#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec4 Color;
in vec2 TexCoord;

uniform sampler2D tex;

void main()
{
	FragColor = texture(tex, TexCoord) * Color;
}";
	}
}
