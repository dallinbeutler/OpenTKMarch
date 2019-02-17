using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace OpenTKmarch
{
    partial class Game
    {
        GameWindow window;
        InputHandler inputHandler;

        public Game(GameWindow window)
        {
            this.window = window;

            window.Load += Window_Load;
            window.RenderFrame += Window_RenderFrame;
            window.Resize += Window_Resize;
            window.UpdateFrame += Window_UpdateFrame;

            inputHandler = new InputHandler(window);
            //window.CursorVisible = false;


            //GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.DepthTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
            window.Run(1f / 60f);
        }

        MyObj myObj;
        Camera camera;// = new Camera(  new Vector3(0, 0, 3f));
        Cube cube;
        Scene scene;
        private void Window_Load(object sender, EventArgs e)
        {
            myObj = new MyObj();
            camera = new Camera(window.Width, window.Height);
            cube = new Cube(camera.shaderProgram);
            var importer = new Assimp.AssimpContext();
            scene = importer.ImportFile("samplescene.blend");
            
        }


        private void Window_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 50f, 0, 50f, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
