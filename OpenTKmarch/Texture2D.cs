using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKmarch
{
    class Texture2D
    {
        public int id;
        public Texture2D(string fileName)
        {
            id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            //set texture wrapping params
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, GLFlags.GL_REPEAT);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, GLFlags.GL_REPEAT);

            //set  texture filtering params. set to nearest for pixelated look
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, GLFlags.GL_LINEAR);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, GLFlags.GL_LINEAR);

            int width, height, nrChannels;

            var texData = ContentPipeline.LoadImage(fileName);

            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texData.Width, texData.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texData.Scan0);
            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width, texData.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, texData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
    }
}
