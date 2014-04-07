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
        internal static List<Key> KeyList = new List<Key>();

        private static MouseState _currentMouseState, _previousMouseState;
        private static KeyboardState _keyboardState;

        internal static void PollKeyboard()
        {
            foreach (var key in InputManager.KeyList)
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
            _currentMouseState = Mouse.GetState();
            if (_currentMouseState != _previousMouseState)
            {
                // Mouse state has changed
                int xdelta = _currentMouseState.X - _previousMouseState.X;
                int ydelta = _currentMouseState.Y - _previousMouseState.Y;
                int zdelta = _currentMouseState.Wheel - _previousMouseState.Wheel;
                Utilities.Camera.AddRotation(xdelta, ydelta);
            }
            _previousMouseState = _currentMouseState;
        }
    }
}
