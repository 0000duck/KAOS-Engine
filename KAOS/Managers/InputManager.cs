using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace KAOS.Managers
{
    /// <summary>
    /// Responsible for polling user input, updating parameters as and when a change is detected.
    /// </summary>
    public static class InputManager
    {
        internal static List<Key> keyList = new List<Key>();

        internal static MouseState current, previous;
        internal static KeyboardState keyState;

        internal static void PollKeyboard()
        {
            foreach (Key key in InputManager.keyList)
            {

                switch (key)
                {
                    case Key.W:
                        Utilities.Camera.Move(0f, 0.1f, 0f);
                        break;

                    case Key.A:
                        Utilities.Camera.Move(-0.1f, 0f, 0f);
                        break;

                    case Key.S:
                        Utilities.Camera.Move(0f, -0.1f, 0f);
                        break;

                    case Key.D:
                        Utilities.Camera.Move(0.1f, 0f, 0f);
                        break;

                    case Key.Q:
                        Utilities.Camera.Move(0f, 0f, 0.1f);
                        break;

                    case Key.E:
                        Utilities.Camera.Move(0f, 0f, -0.1f);
                        break;

                    case Key.F1:
                        Utilities.Renderer.ToggleWireframeOn();
                        break;

                    case Key.F2:
                        Utilities.Renderer.ToggleWireframeOff();
                        break;
                    default:
                        break;
                }
            }
        }

        internal static void PollMouse()
        {
            current = Mouse.GetState();
            if (current != previous)
            {
                // Mouse state has changed
                int xdelta = current.X - previous.X;
                int ydelta = current.Y - previous.Y;
                int zdelta = current.Wheel - previous.Wheel;
                Utilities.Camera.AddRotation(xdelta, ydelta);
            }
            previous = current;
        }
    }
}
