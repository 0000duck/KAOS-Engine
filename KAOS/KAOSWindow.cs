using KAOS.Managers;
using KAOS.Utilities;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KAOS
{
    /// <summary>
    /// Inherit from here to get started.
    /// This is the main interface to the system.
    /// </summary>
    public abstract class KAOSWindow : GameWindow, IDisposable
    {

        public static string AppName { get { return "AWEngine"; } }

        public int ScreenWidth { get { return this.ClientSize.Width; } }
        public int ScreenHeight { get { return this.ClientSize.Height; } }

        protected Matrix4 projectionMatrix, modelviewMatrix;
        protected AnimationTimer m_Timer;
        
        public KAOSWindow(int height, int width, int major, int minor)
            : base(height, width, new GraphicsMode(32, 16, 0, 4), KAOSWindow.AppName, GameWindowFlags.Default, 
            DisplayDevice.Default, major, minor, GraphicsContextFlags.Default)
        { }

        #region Load everything here
        protected override void OnLoad(System.EventArgs e)
        {
            BaseInitialisation();
            Initialise();
        }

        private void BaseInitialisation()
        {
            InitialiseTimer();
            InitialiseInput();
            InitialiseStockShaders();
        }

        private void InitialiseInput()
        {
            Keyboard.KeyDown += HandleKeyDown;
            Keyboard.KeyUp += HandleKeyUp;
        }

        private void InitialiseTimer()
        {
            m_Timer = new AnimationTimer();
        }

        private void InitialiseStockShaders()
        {
            ShaderManager.LoadDefaultSkyboxShader();
            ShaderManager.LoadDefaultRenderShader();
        }

        public abstract void Initialise();

        #endregion

        MouseState current, previous;

        #region Game Loop
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            #region Mouse Input
            current = OpenTK.Input.Mouse.GetState();
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
                ResetCursor();
            }
            
            #endregion

            UpdateFrame(m_Timer.GetElapsedTime());
        }

        new public abstract void UpdateFrame(float elapsedTime);

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Viewport(0, 0, ScreenWidth, ScreenHeight);

            Title = KAOSWindow.AppName +

                " OpenGL: " + GL.GetString(StringName.Version) +
                " GLSL: " + GL.GetString(StringName.ShadingLanguageVersion) +
                " FPS: " + string.Format("{0:F}", 1.0 / e.Time);

            

            RenderFrame(m_Timer.GetElapsedTime());

            SwapBuffers();
        }

        new public abstract void RenderFrame(float elapsedTime);

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ScreenWidth, ScreenHeight);

            float aspect = ScreenWidth / (float)ScreenHeight;

            #region Assimp Example Code
            //float widthToHeight = ScreenWidth / (float)ScreenHeight;
            //Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, widthToHeight, 1, 64);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadMatrix(ref perspective); 
            #endregion
        }
        #endregion

        #region Input Control
        
        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();
            InputManager.keyList.Add(e.Key);
        }

        private void HandleKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            for (int count = 0; count < InputManager.keyList.Count; count++)
            {
                if (InputManager.keyList[count] == e.Key)
                {
                    InputManager.keyList.Remove(InputManager.keyList[count]);
                }
            }
        }

        public void ResetCursor()
        {
            System.Windows.Forms.Cursor.Position = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        } 
        
        #endregion

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}