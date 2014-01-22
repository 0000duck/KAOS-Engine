using AWGL.Nodes;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
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
    public class AWScene : GameWindow, IDisposable
    {
        #region Members
        int modelviewMatrixLocation,
            projectionMatrixLocation,
            vaoHandle,
            positionVboHandle,
            normalVboHandle,
            eboHandle;

        Matrix4 projectionMatrix, modelviewMatrix;

        AWShaderManager shaderManager;

        AWNode m_sceneGraph;
        AWGroupNode root;
        AWGroupNode group;
        AWCube cube;
        AWGraphLines graph;
        AWCamera camera;
        #endregion

        public AWScene()
            : base(1024, 680, new GraphicsMode(32, 24, 0, 4), AWEngine.AppName, GameWindowFlags.Default, 
            DisplayDevice.Default, 3, 0, GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug)
        { }

        #region OpenGL Setup
        protected override void OnLoad(System.EventArgs e)
        {
            VSync = VSyncMode.On;
            camera = new AWCamera();
            root = new AWGroupNode();
            group = new AWGroupNode();
            cube = new AWCube();
            graph = new AWGraphLines(20);
            CreateShaders();
            CreateVBOs();
            CreateVAOs();

            // Other state
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(Color.CornflowerBlue);

#if Debug
            AWLogger.WriteLine("...Exiting OnLoad"); 
#endif
        }

        #region Create Shaders
        void CreateShaders()
        {
            shaderManager = new AWShaderManager("opentk-vs", "opentk-fs");

            GL.UseProgram(shaderManager.ProgramHandle);

            shaderManager.SetUniforms(
                out projectionMatrixLocation, out modelviewMatrixLocation,
                out projectionMatrix, modelviewMatrix, ClientSize, ref camera
            );
        }
        #endregion

        #region Create VBOs
        void CreateVBOs()
        {

            positionVboHandle = AWBufferManager.SetupBuffer(
                graph.Vertices, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
                );

            normalVboHandle = AWBufferManager.SetupBuffer(
                graph.Vertices, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
                );

            //eboHandle = AWBufferManager.SetupBuffer(
            //    cube.Indices, BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw
            //    );

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
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
            vaoHandle = AWBufferManager.GenerateVaoBuffer();

            #region add matrix transform uniforms

            AWBufferManager.SetupVaoBuffer(positionVboHandle,

                shaderManager.ProgramHandle, 0, 3, "in_position",
                BufferTarget.ArrayBuffer, VertexAttribPointerType.Float
                );
            AWBufferManager.SetupVaoBuffer(normalVboHandle,

                shaderManager.ProgramHandle, 1, 3, "in_normal",
                BufferTarget.ArrayBuffer, VertexAttribPointerType.Float
                );

            #endregion

            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);

            GL.BindVertexArray(0);
        }
        #endregion 
        #endregion

        #region MAIN LOOP
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Focused)
            {
                Point center = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
                Point delta = new Point(center.X - System.Windows.Forms.Cursor.Position.X, center.Y - System.Windows.Forms.Cursor.Position.Y);

                camera.AddRotation(delta.X, delta.Y);
                ResetCursor();
            }


            Matrix4 lookat = camera.GetViewMatrix();
            GL.UniformMatrix4(modelviewMatrixLocation, false, ref lookat);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(vaoHandle);
            GL.DrawArrays(PrimitiveType.Lines, 0, 20);
                //Elements(
                //PrimitiveType.Lines, cube.Indices.Length,
                //DrawElementsType.UnsignedInt, IntPtr.Zero
                //);

            SwapBuffers();
        } 
        #endregion

        #region GameWindow.Dispose
        public override void Dispose()
        {
            base.Dispose();
            shaderManager.Dispose();
        } 
        #endregion

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == 27)
            {
                Exit();
            }

            switch (e.KeyChar)
            {
                case 'w':
                    camera.Move(0f, 0.1f, 0f);
                    break;
                case 'a':
                    camera.Move(-0.1f, 0f, 0f);
                    break;
                case 's':
                    camera.Move(0f, -0.1f, 0f);
                    break;
                case 'd':
                    camera.Move(0.1f, 0f, 0f);
                    break;
                case 'q':
                    camera.Move(0f, 0f, 0.1f);
                    break;
                case 'e':
                    camera.Move(0f, 0f, -0.1f);
                    break;
                case 'f':
                    this.WindowState = (this.WindowState != WindowState.Fullscreen) 
                        ? WindowState.Fullscreen: WindowState.Normal;
                    break;
                case 'x':
                    Exit();
                    break;
            }
        }

        void ResetCursor()
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

    }
}
