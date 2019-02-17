using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKmarch
{
    partial class Game
    {

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(System.Drawing.Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            myObj.draw(camera);
            //scene.Meshes.First().Faces.First().
            //cube.Render(camera.shaderProgram);
            //scene.Meshes.First().Vertices.First().

            window.SwapBuffers();
        }
    }
}
