using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace OpenTKmarch
{
    partial class Game
    {
        Stopwatch stopwatch = new Stopwatch();
        float deltaTime = 0.01f;
        float lastFrame = 0.0f;

        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            deltaTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            inputHandler.update();
            HandleInput();

            float centerx = window.Bounds.Width / 2;
            float centery = window.Bounds.Height / 2;

            camera.ProcessMouseMovement(-inputHandler.mouseDelta.X, -inputHandler.mouseDelta.Y);
        }

        public void HandleInput()
        {
            if (inputHandler.forward)
                camera.ProcessKeyboard(Camera.Camera_Movement.FORWARD, deltaTime);
            else if (inputHandler.backward)
                camera.ProcessKeyboard(Camera.Camera_Movement.BACKWARD, deltaTime);
            else if (inputHandler.left)
                camera.ProcessKeyboard(Camera.Camera_Movement.LEFT, deltaTime);
            else if (inputHandler.right)
                camera.ProcessKeyboard(Camera.Camera_Movement.RIGHT, deltaTime);

        }
    }
}
