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

        internal static void PollInput()
        {
            #region Keyboard
            keyState = Keyboard.GetState();

            #endregion

            #region Mouse
            current = Mouse.GetState();
            if (current[MouseButton.Left])
            {
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

            #endregion
        }
    }
}
