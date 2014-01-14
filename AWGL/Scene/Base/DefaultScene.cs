using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

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
            Keyboard.KeyDown += Keyboard_KeyDown;
        }

        private Color4 m_backgroundColor = new Color4(.1f, 0f, .1f, 0f);

        #region Camera
        protected float m_eyeX = .0f;
        protected float m_eyeY = 10.0f;
        protected float m_eyeZ = 10.0f;
        #endregion

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

            Setup(e);
        }
        #endregion

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

        #region Input
        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <param name="sender">The KeyboardDevice which generated this event.</param>
        /// <param name="e">The key that was pressed.</param>
        protected void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {

            switch (e.Key)
            {
                #region Window Controls

                case Key.Escape: this.Exit();
                    break;
                case Key.F11:
                    if (this.WindowState == WindowState.Fullscreen)
                        this.WindowState = WindowState.Normal;
                    else
                        this.WindowState = WindowState.Fullscreen;
                    break;

                #endregion

                #region Camera Controls

                case Key.Up: m_eyeY += 2f;
                    break;
                case Key.Down: m_eyeY += -2f;
                    break;
                case Key.Right: m_eyeX += 2f;
                    break;
                case Key.Left: m_eyeX += -2f;
                    break;

                #endregion

            }   
        }
        #endregion

    }
}