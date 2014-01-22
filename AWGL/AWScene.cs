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
        #endregion

        public AWScene()
            : base(1366, 768, new GraphicsMode(32, 24, 0, 4), AWEngine.AppName, GameWindowFlags.Fullscreen, 
            DisplayDevice.Default, 3, 0, GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug)
        { }

        #region OpenGL Setup
        protected override void OnLoad(System.EventArgs e)
        {
            VSync = VSyncMode.On;

            root = new AWGroupNode();
            group = new AWGroupNode();
            cube = new AWCube();

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
                out projectionMatrix, out modelviewMatrix, ClientSize
            );
        }
        #endregion

        #region Create VBOs
        void CreateVBOs()
        {

            positionVboHandle = AWBufferManager.SetupBuffer(
                AWCube.Vertices, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
                );

            normalVboHandle = AWBufferManager.SetupBuffer(
                AWCube.Vertices, BufferTarget.ArrayBuffer, BufferUsageHint.StaticDraw
                );

            eboHandle = AWBufferManager.SetupBuffer(
                AWCube.Indices, BufferTarget.ElementArrayBuffer, BufferUsageHint.StaticDraw
                );

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

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);

            GL.BindVertexArray(0);
        }
        #endregion 
        #endregion

        #region MAIN LOOP
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Matrix4 rotation = Matrix4.CreateRotationY((float)e.Time);
            Matrix4.Mult(ref rotation, ref modelviewMatrix, out modelviewMatrix);
            GL.UniformMatrix4(modelviewMatrixLocation, false, ref modelviewMatrix);

            if (Keyboard[OpenTK.Input.Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(vaoHandle);
            GL.DrawElements(
                PrimitiveType.Triangles, AWCube.Indices.Length,
                DrawElementsType.UnsignedInt, IntPtr.Zero
                );

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

    }
}
