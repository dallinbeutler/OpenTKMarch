using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace OpenTKmarch
{
    class Cube
    {
    static string vertexShaderSource =
@"
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0f);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}
";

       static string fragmentShaderSource =
    @"
#version 330 core
out vec4 FragColor;

in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}
";
        // set up vertex data (and buffer(s)) and configure vertex attributes
        // ------------------------------------------------------------------
    static float []vertices = {
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
    };
        // world space positions of our cubes
    static Vector3 []cubePositions = {
        new Vector3( 0.0f,  0.0f,  0.0f),
        new Vector3( 2.0f,  5.0f, -15.0f),
        new Vector3(-1.5f, -2.2f, -2.5f),
        new Vector3(-3.8f, -2.0f, -12.3f),
        new Vector3( 2.4f, -0.4f, -3.5f),
        new Vector3(-1.7f,  3.0f, -7.5f),
        new Vector3( 1.3f, -2.0f, -2.5f),
        new Vector3( 1.5f,  2.0f, -2.5f),
        new Vector3( 1.5f,  0.2f, -1.5f),
        new Vector3(-1.3f,  1.0f, -1.5f)
    };
        uint vbo, vao;

        Texture2D texture1 = new Texture2D(System.IO.Directory.GetCurrentDirectory() + @"\Content\smile.png");

        Texture2D texture2 = new Texture2D(System.IO.Directory.GetCurrentDirectory() + @"\Content\container.jpg");
        //public ShaderProgram shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);

        public Cube(ShaderProgram program)
        {
            GL.GenVertexArrays(1, out vbo);
            GL.GenBuffers(1, out vbo);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.bytesize(), vertices, BufferUsageHint.StaticDraw);

            //apply the position attributes
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //apply the texture coordinate attributes
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);


            //tex1= GL.GenTexture();
            //GL.BindTexture(TextureTarget.Texture2D, tex1);

            ////set texture wrapping params
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, GLFlags.GL_REPEAT);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, GLFlags.GL_REPEAT);

            ////set  texture filtering params. set to nearest for pixelated look
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, GLFlags.GL_LINEAR);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, GLFlags.GL_LINEAR);

            //int width, height, nrChannels;

            //var texData = ContentPipeline.LoadImage("container.jpg");

            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width, texData.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, texData.Scan0);
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }

        public void Render(ShaderProgram program)
        {

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture1.id);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, texture2.id);
            for (uint i = 0; i < 10; i++)
            {
                var model = Matrix4.CreateTranslation(cubePositions[i]);
                float angle = 20f * i;
                model *= Matrix4.CreateRotationX(i);
                program.SetMat4("model", ref model);
                
                //GL.DrawArrays(BeginMode.Triangles,0,36);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }
        }

    }
}
