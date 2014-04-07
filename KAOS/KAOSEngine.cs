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
    /// This is the main interface to the system. Inherit from here to get started.
    /// </summary>
    public abstract class KAOSEngine : GameWindow, IDisposable
    {
        protected StateManager stateManager = new StateManager();

        public static string AppName { get { return "KAOS-Engine"; } }

        protected int ScreenWidth { get { return this.ClientSize.Width; } }
        protected int ScreenHeight { get { return this.ClientSize.Height; } }

        protected AnimationTimer m_Timer;
        protected Vector2 lastMousePos = new Vector2();
        
        public KAOSEngine(int height, int width, int major, int minor)
            : base(height, width, new GraphicsMode(32, 16, 0, 4), KAOSEngine.AppName, GameWindowFlags.Default, 
            DisplayDevice.Default, major, minor, GraphicsContextFlags.Default | GraphicsContextFlags.ForwardCompatible)
        { }

        // Load everything here
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
            ShaderManager.LoadDefaultAssimpShader();
        }

        public abstract void Initialise();

        // Game Loop
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputManager.PollKeyboard();
            if (Focused)
            {
                Vector2 delta = lastMousePos - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

                Camera.AddRotation(delta.X, delta.Y);
                ResetCursor();
            }

            double time = e.Time;

            UpdateFrame((float)time);
        }

        new public abstract void UpdateFrame(float elapsedTime);

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Viewport(0, 0, ScreenWidth, ScreenHeight);

            GL.ClearBuffer(ClearBuffer.Color, 0, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.ClearBuffer(ClearBuffer.Depth, 0, new float[] { 1.0f });

            Title = KAOSEngine.AppName +

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

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
        
        // Input Control

        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Exit();
                    break;

                case Key.Number0:
                    stateManager.ChangeState("ModelScene");
                    break;

                case Key.Number1:
                    stateManager.ChangeState("SceneGraphScene");
                    break;

                case Key.Number2:
                    stateManager.ChangeState("SkyboxScene");
                    break;

                default:
                    break;
            }

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

        protected void SetState(string stateToLoad)
        {
            stateManager.ChangeState(stateToLoad);
        }

        public void ResetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            lastMousePos = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        } 
        
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}