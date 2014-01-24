using AWGL.Managers;
using AWGL.Nodes;
using AWGL.Utilities;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AWGL
{
    /// <summary>
    /// Inherit me
    /// </summary>
    public abstract class AWEngineWindow : GameWindow, IDisposable
    {
        #region Old code
        public static string AppName
        {
            get
            {
                return "AWEngine";
            }

        }
        int modelviewMatrixLocation,
            projectionMatrixLocation,
            vaoHandle,
            positionVboHandle,
            normalVboHandle,
            eboHandle;

        Matrix4 projectionMatrix, modelviewMatrix;

        ShaderManager shaderManager;

        AWNode m_sceneGraph;
        AWGroupNode root;
        AWGroupNode group;
        AWCube cube;
        AWGraphLines graph;
        #endregion

        protected PreciseTimer m_Timer;
        protected Camera camera;
        protected List<Key> keyList;

        public int ScreenWidth { get { return this.ClientSize.Width; } }
        public int ScreenHeight { get { return this.ClientSize.Height; } }

        public AWEngineWindow(int height, int width)
            : base(height, width, new GraphicsMode(32, 24, 0, 4), AWEngineWindow.AppName, GameWindowFlags.Default, 
            DisplayDevice.Default, 3, 0, GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug)
        { }

        #region Load everything here
        protected override void OnLoad(System.EventArgs e)
        {
            m_Timer = new PreciseTimer();

            //CameraManager
            camera = new Camera();
            
            // InputManager
            keyList = new List<Key>();
            Keyboard.KeyDown += HandleKeyDown;
            Keyboard.KeyUp += HandleKeyUp;

            #region Old Code
            //root = new AWGroupNode();
            //group = new AWGroupNode();
            //cube = new AWCube();
            //graph = new AWGraphLines(20);
            //CreateShaders();
            //CreateVBOs();
            //CreateVAOs();

            //// Other state
            //GL.Enable(EnableCap.DepthTest);
            //GL.ClearColor(Color.CornflowerBlue); 
            #endregion

#if Debug
            AWLogger.WriteLine("...Exiting OnLoad"); 
#endif      
            Initialise();
        }

        public abstract void Initialise();
        #endregion

        #region Old Code
        #region Create Shaders
        void CreateShaders()
        {
            //shaderManager = new ShaderManager("opentk-vs", "opentk-fs");

            //GL.UseProgram(shaderManager.ProgramHandle);

            //shaderManager.SetUniforms(
            //    out projectionMatrixLocation, out modelviewMatrixLocation,
            //    out projectionMatrix, modelviewMatrix, ClientSize, ref camera
            //);
        }
        #endregion

        #region Create VBOs
        void CreateVBOs()
        {
            //Vector3[] aggregateVerts = new Vector3[graph.Vertices.Length + cube.Vertices.Length];
            //System.Array.Copy(graph.Vertices, aggregateVerts, graph.Vertices.Length);
            //System.Array.Copy(cube.Vertices, 0, aggregateVerts, graph.Vertices.Length, cube.Vertices.Length);

            //positionVboHandle = BufferManager.SetupBuffer(
            //    aggregateVerts, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
            //    );

            //normalVboHandle = BufferManager.SetupBuffer(
            //    aggregateVerts, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
            //    );

            //eboHandle = BufferManager.SetupBuffer(
            //    cube.Indices, BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw
            //    );

            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
        #endregion

        #region Create VAOs
        void CreateVAOs()
        {
            #region ---
            // GL3 allows us to store the vertex layout in a "vertex array object" (VAO).
            // This means we do not have to re-issue VertexAttribPointer calls
            // every time we try to use a different vertex layout - these calls are
            // stored in the VAO so we simply need to bind the correct VAO.

            #endregion

            // generate
            vaoHandle = BufferManager.GenerateVaoBuffer();

            #region add matrix transform uniforms

            //BufferManager.SetupVaoBuffer(positionVboHandle,

            //    shaderManager.ProgramHandle, 0, 3, "in_position",
            //    BufferTarget.ArrayBuffer, VertexAttribPointerType.Float
            //    );
            //BufferManager.SetupVaoBuffer(normalVboHandle,

            //    shaderManager.ProgramHandle, 1, 3, "in_normal",
            //    BufferTarget.ArrayBuffer, VertexAttribPointerType.Float
            //    );

            #endregion

            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);

            //GL.BindVertexArray(0);
        }
        #endregion  
        #endregion

        #region Game Loop
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            #region Input
            //if (Focused)
            //{
            //    Point center = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            //    Point delta = new Point(center.X - System.Windows.Forms.Cursor.Position.X, center.Y - System.Windows.Forms.Cursor.Position.Y);

            //    camera.AddRotation(delta.X, delta.Y);
            //    ResetCursor();
            //}

            MoveCamera(); 
            #endregion

            #region Old Code
            //Matrix4 lookat = camera.GetViewMatrix();
            //GL.UniformMatrix4(modelviewMatrixLocation, false, ref lookat); 
            #endregion
        }

        new public abstract void UpdateFrame(float elapsedTime);

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            Title = AWEngineWindow.AppName + " - FPS: " + string.Format("{0:F}", 1.0 / e.Time);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region Old Code
            //GL.BindVertexArray(vaoHandle);
            //GL.DrawArrays(PrimitiveType.Lines, 0, 20);
            //GL.DrawArrays(PrimitiveType.Triangles, 20, cube.Indices.Length);
            //PrimitiveType.Lines, cube.Indices.Length,
            //DrawElementsType.UnsignedInt, IntPtr.Zero
            //); 
            #endregion

            // Single call to StateRenderer to take place here.
            RenderFrame(m_Timer.GetElapsedTime());

            SwapBuffers();
        }

        new public abstract void RenderFrame(float elapsedTime);

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ScreenWidth, ScreenHeight);

            #region Old Code
            //Matrix4 lookat = camera.GetViewMatrix();
            //GL.UniformMatrix4(modelviewMatrixLocation, false, ref lookat); 
            #endregion
        }
        #endregion

        #region GameWindow.Dispose
        public override void Dispose()
        {
            base.Dispose();
            //shaderManager.Dispose();
        } 
        #endregion

        #region Input Control
        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keyList.Add(e.Key);
        }

        private void HandleKeyUp(object sender, KeyboardKeyEventArgs e)
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
            foreach (Key key in keyList)
            {

                switch (key)
                {
                    case Key.Escape:
                        Exit();
                        break;

                    case Key.W:
                        camera.Move(0f, 0.1f, 0f);
                        break;

                    case Key.A:
                        camera.Move(-0.1f, 0f, 0f);
                        break;

                    case Key.S:
                        camera.Move(0f, -0.1f, 0f);
                        break;

                    case Key.D:
                        camera.Move(0.1f, 0f, 0f);
                        break;

                    case Key.Q:
                        camera.Move(0f, 0f, 0.1f);
                        break;

                    case Key.E:
                        camera.Move(0f, 0f, -0.1f);
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

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            //if (Focused)
            //{
            //    ResetCursor();
            //}
        } 
        #endregion
    }
}
