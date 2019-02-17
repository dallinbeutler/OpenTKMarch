using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;

namespace OpenTKmarch
{
    class Program
    {

        static void Main(string[] args)
        {
            Game g = new Game(new GameWindow(1680,1050, GraphicsMode.Default));
            //Assimp.Sample.SimpleOpenGLSample simpleOpenGLSample = new Assimp.Sample.SimpleOpenGLSample();
            //simpleOpenGLSample.Run();
        }
    }
}
