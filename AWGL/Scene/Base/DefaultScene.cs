using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AWGL.Scene
{
    /// <summary>
    /// Controls Main Window functions and sets up OpenGL
    /// </summary>
    public abstract class DefaultScene : GameWindow
    {
        public DefaultScene()
            : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {
            //this.WindowState = WindowState.Fullscreen;
        }

        private Color4 m_backgroundColor = new Color4(.1f, 0f, .1f, 0f);

        protected AWCamera camera;
        private List<Key> keyList;

        #region OnLoad
        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Console.WriteLine("");
            Console.WriteLine("Video informations :");
            Console.WriteLine("Graphics card vendor : {0}", GL.GetString(StringName.Vendor));
            Console.WriteLine("Renderer : {0}", GL.GetString(StringName.Renderer));
            Console.WriteLine("Version : {0}", GL.GetString(StringName.Version));
            Console.WriteLine("Shading Language Version : {0}", GL.GetString(StringName.ShadingLanguageVersion));
            TestOpenGLVersion();

            Title = "AWGL: High level OpenTK wrapper - " + GL.GetString(StringName.Renderer) + " (GL " + GL.GetString(StringName.Version) + ")";

            GL.ClearColor(Color4.Gray);

            camera = new AWCamera();
            keyList = new List<Key>();
            Keyboard.KeyDown += HandleKeyDown;
            Keyboard.KeyUp += HandleKeyUp;
            Setup(e);
        }
        #endregion

        void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keyList.Add(e.Key);
        }

        void HandleKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            for (int count = 0; count < keyList.Count; count++)
            {
                if (keyList[count] == e.Key)
                {
                    keyList.Remove(keyList[count]);
                }
            }
        }

        private void MoveCamera()
        {
            foreach (OpenTK.Input.Key key in keyList)
            {

                switch (key)
                {
                    case OpenTK.Input.Key.Escape:
                        Exit();
                        break;

                    case OpenTK.Input.Key.W:
                        camera.Move(0f, 0.1f, 0f);
                        break;

                    case OpenTK.Input.Key.A:
                        camera.Move(-0.1f, 0f, 0f);
                        break;

                    case OpenTK.Input.Key.S:
                        camera.Move(0f, -0.1f, 0f);
                        break;

                    case OpenTK.Input.Key.D:
                        camera.Move(0.1f, 0f, 0f);
                        break;

                    case OpenTK.Input.Key.Q:
                        camera.Move(0f, 0f, 0.1f);
                        break;

                    case OpenTK.Input.Key.E:
                        camera.Move(0f, 0f, -0.1f);
                        break;

                    default:
                        break;
                }
            }
        }

        #region OnResize
        /// <summary>
        /// Respond to resize events here.
        /// </summary>
        /// <param name="e">Contains information on the new GameWindow size.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Resize(e);
        }

        #endregion

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
 	         base.OnUpdateFrame(e);
             
             if (Focused)
             {
                 MoveCamera();

                 Point center = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
                 Point delta = new Point(center.X - System.Windows.Forms.Cursor.Position.X, center.Y - System.Windows.Forms.Cursor.Position.Y);

                 camera.AddRotation(delta.X, delta.Y);
                 ResetCursor();
             }
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        }

        public abstract void Setup(EventArgs e);

        new public abstract void Resize(EventArgs e);

        #region TestOpenGLVersion
        /// <summary>
        /// Get OpenGL Version Information and check system meets requirements
        /// </summary>
        private void TestOpenGLVersion()
        {
            Version m_Version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            Version m_TargetLow = new Version(3, 1);
            Version m_TargetHigh = new Version(4, 1);
            if (m_Version < m_TargetLow)
            {
                throw new NotSupportedException(String.Format(
                    "OpenGL {0} is required (you only have {1}).", m_TargetLow, m_Version));
            }
            else if (m_Version > m_TargetHigh)
            {
                throw new NotSupportedException(String.Format(
                    "OpenGL {0} is required (you only have {1}).", m_TargetHigh, m_Version));
            }
        }
        #endregion

        void ResetCursor()
        {
            System.Windows.Forms.Cursor.Position = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
        }
    }
}