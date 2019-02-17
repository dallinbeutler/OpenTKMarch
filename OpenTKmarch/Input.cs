using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;


namespace OpenTKmarch
{
    class InputHandler
    {
        GameWindow window;
        private Vector2 lastMousePos = new Vector2();
        public Vector2 mouseDelta;

        public InputHandler(GameWindow window)
        {
            this.window = window;

            window.KeyPress += Window_KeyPress;
            window.KeyDown += Window_KeyDown;
            window.KeyUp += Window_KeyUp;
            window.FocusedChanged += Window_FocusedChanged;
        }

        public void update()
        {
            if (window.Focused)
            {
                mouseDelta = lastMousePos - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

                //camera.ProcessMouseMovement(-delta.X, -delta.Y);
                ResetCursor();
            }
            else
            {
                mouseDelta = new Vector2();
            }
        }


        public bool forward, left, backward, right, flight= false;
        private void Window_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case OpenTK.Input.Key.Escape:
                    window.Close();
                    break;
                case OpenTK.Input.Key.W:
                    forward = true;
                    break;
                case OpenTK.Input.Key.A:
                    left = true;
                    break;
                case OpenTK.Input.Key.S:
                    backward = true;
                    break;
                case OpenTK.Input.Key.D:
                    right = true;
                    break;
                case Key.Number1:
                    flight = !flight;
                    break;
            }
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Window_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case OpenTK.Input.Key.Escape:
                    window.Close();
                    break;
                case OpenTK.Input.Key.W:
                    forward = false;
                    break;
                case OpenTK.Input.Key.A:
                    left = false;
                    break;
                case OpenTK.Input.Key.S:
                    backward = false;
                    break;
                case OpenTK.Input.Key.D:
                    right = false;
                    break;
            }
        }

        private void Window_FocusedChanged(object sender, EventArgs e)
        {
            if (window.Focused)
            {
                ResetCursor();
            }
        }

        void ResetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(window.Bounds.Left + window.Bounds.Width / 2, window.Bounds.Top + window.Bounds.Height / 2);
            lastMousePos = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }
    }
}
