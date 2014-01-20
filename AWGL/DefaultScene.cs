using AWGL.Shapes;
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
    public class DefaultScene : GameWindow
    {
        #region Constructor
        
        public DefaultScene()
            : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {
            //set context
            this.WindowState = WindowState.Fullscreen;
            m_backgroundColor= new Color4(.1f, 0f, .1f, 0f);
            
            //create player camera
            playerView = new AWCamera();

            //register key list
            keyList = new List<Key>();
            Keyboard.KeyDown += HandleKeyDown;
            Keyboard.KeyUp += HandleKeyUp;

            //InitialiseNodes
            worldRoot = new AWGroupNode();
            landRoot = new AWGroupNode();
            graph = new AWGraphLines(); ;
            cube = new AWCube();
            knot = new TorusKnot( 256, 32, 0.1, 3, 4, 1, true );

            //create scenegraph
            worldRoot.AddChild(graph);
            worldRoot.AddChild(landRoot);

            landRoot.SetTranslation(0, .5, -10);
            landRoot.AddChild(cube);

            m_sceneGraph = worldRoot;

            m_hook1 = landRoot;

            cubePosY = 1.5f;

            shaderManager = new AWShaderManager();
            GL.UseProgram(shaderManager.programID());
        }

        #endregion

        #region Fields

        protected AWCamera playerView;

        private Color4 m_backgroundColor;
        private List<Key> keyList;

        //scenegraph
        private AWNode m_sceneGraph;
        private AWGroupNode worldRoot, landRoot;
        private AWGraphLines graph;
        private AWCube cube;
        private TorusKnot knot;
        private AWGroupNode m_hook1;

        private const float m_rotationspeed = 180.0f;
        private float m_spinangle, cubePosY;

        private AWShaderManager shaderManager;

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
            AWUtils.TestOpenGLVersion();

            Title = "AWGL Engine Prototype      - " + GL.GetString(StringName.Renderer) + " (GL " + GL.GetString(StringName.Version) + ")";

            GL.ClearColor(m_backgroundColor);

            GL.Enable(EnableCap.DepthTest);
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
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = playerView.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }
        #endregion

        #region OnFocusChanged

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
            {
                ResetCursor();
            }
        }

        #endregion

        #region OnUpdateFrame

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Focused)
            {
                MoveCamera();

                Point center = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
                Point delta = new Point(center.X - System.Windows.Forms.Cursor.Position.X, center.Y - System.Windows.Forms.Cursor.Position.Y);

                playerView.AddRotation(delta.X, delta.Y);
                ResetCursor();
            }
        }

        #endregion

        #region OnRenderFrame

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            m_spinangle += m_rotationspeed * (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = playerView.GetViewMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            m_hook1.SetRotation(m_spinangle, 0, 1, 0);

            m_sceneGraph.Render();

            SwapBuffers();
        }

        #endregion

        #region Input & Camera

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
                        playerView.Move(0f, 0.1f, 0f);
                        break;

                    case OpenTK.Input.Key.A:
                        playerView.Move(-0.1f, 0f, 0f);
                        break;

                    case OpenTK.Input.Key.S:
                        playerView.Move(0f, -0.1f, 0f);
                        break;

                    case OpenTK.Input.Key.D:
                        playerView.Move(0.1f, 0f, 0f);
                        break;

                    case OpenTK.Input.Key.Q:
                        playerView.Move(0f, 0f, 0.1f);
                        break;

                    case OpenTK.Input.Key.E:
                        playerView.Move(0f, 0f, -0.1f);
                        break;

                    case OpenTK.Input.Key.Up:
                        landRoot.SetTranslation(0, cubePosY += .1f, -10);
                        break;

                    case OpenTK.Input.Key.Down:
                        landRoot.SetTranslation(0, cubePosY += -.1f, -10);
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void ResetCursor()
        {
            System.Windows.Forms.Cursor.Position = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
        }

        #endregion

    }
}