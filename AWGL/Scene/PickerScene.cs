﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using AWGL.Shapes;
using System.Drawing;
using System.IO;

namespace AWGL.Scene
{
    /// <summary>
    /// This demo shows over which triangle the cursor is, it does so by assigning all 3 vertices of a triangle the same Ids.
    /// Each Id is a uint, split into 4 bytes and used as triangle color. In an extra pass, the screen is cleared to uint.MaxValue,
    /// and then the mesh is drawn using color. Using GL.ReadPixels() the value under the mouse cursor is read and can be converted.
    /// </summary>
    public class PickerScene : DefaultScene
    {
        public PickerScene()
        {
            this.VSync = VSyncMode.Off;
        }

        #region Private Member variable
        private const TextureTarget Target = TextureTarget.TextureRectangleArb;
        private float angle;
        private BeginMode VBO_PrimMode;
        private Vertex[] VBO_Array;
        uint VBO_Handle;

        private uint SelectedTriangle;

        private int VertexShaderObject, FragmentShaderObject, ProgramObject;
        #endregion

        #region OnLoad
        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            #region prepare data for VBO from procedural object
            DrawableShape temp_obj = new SierpinskiTetrahedron(3f, SierpinskiTetrahedron.eSubdivisions.Five, false);
            VertexT2fN3fV3f[] temp_VBO;
            uint[] temp_IBO;
            temp_obj.GetArraysforVBO(out VBO_PrimMode, out temp_VBO, out temp_IBO);
            temp_obj.Dispose();
            if (temp_IBO != null)
                throw new Exception("Expected data for GL.DrawArrays, but Element Array is not null.");

            // Convert from temp mesh to final object, copy position and add triangle Ids for the color attribute.
            VBO_Array = new Vertex[temp_VBO.Length];
            int TriangleCounter = -1;
            for (int i = 0; i < temp_VBO.Length; i++)
            {
                // Position
                VBO_Array[i].Position = temp_VBO[i].Position;

                // Index
                if (i % 3 == 0)
                    TriangleCounter++;
                VBO_Array[i].Color = new Byte4(BitConverter.GetBytes(TriangleCounter));
            }
            #endregion prepare data for VBO from procedural object

            #region Setup VBO for drawing
            GL.GenBuffers(1, out VBO_Handle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_Handle);
            GL.BufferData<Vertex>(BufferTarget.ArrayBuffer, (IntPtr)(VBO_Array.Length * Vertex.SizeInBytes), VBO_Array, BufferUsageHint.StaticDraw);
            GL.InterleavedArrays(InterleavedArrayFormat.C4ubV3f, 0, IntPtr.Zero);

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Trace.WriteLine("VBO Setup failed (Error: " + err + "). Attempting to continue.");
            #endregion Setup VBO for drawing

            #region Shader

            ProgramObject = GL.CreateProgram();

            // Load&Compile Vertex Shader
            LoadShader("Picking_VS.glsl", ShaderType.VertexShader, ProgramObject, out VertexShaderObject);

            // Load&Compile Fragment Shader
            LoadShader("Picking_FS.glsl", ShaderType.FragmentShader, ProgramObject, out FragmentShaderObject);
            
            // Link the Shaders to a usable Program

            // link it all together
            GL.LinkProgram(ProgramObject);

            err = GL.GetError();
            if (err != ErrorCode.NoError)
                Trace.WriteLine("LinkProgram: " + err);

            GL.UseProgram(ProgramObject);

            err = GL.GetError();
            if (err != ErrorCode.NoError)
                Trace.WriteLine("UseProgram: " + err);

            // flag ShaderObjects for delete when not used anymore
            GL.DeleteShader(VertexShaderObject);
            GL.DeleteShader(FragmentShaderObject);

            int temp;
            string LogInfo;
            GL.GetProgram(ProgramObject, ProgramParameter.LinkStatus, out temp);
            Trace.WriteLine("Linking Program (" + ProgramObject + ") " + ((temp == 1) ? "succeeded." : "FAILED!"));
            if (temp != 1)
            {
                GL.GetProgramInfoLog(ProgramObject, out LogInfo);
                Trace.WriteLine("Program Log:\n" + LogInfo);
            }

            Trace.WriteLine("End of Shader build. GL Error: " + GL.GetError());

            GL.UseProgram(0);

            #endregion Shader

        }
        #endregion

        #region OnUnload
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffers(1, ref VBO_Handle);

            base.OnUnload(e);
        }
        #endregion

        #region OnResize
        /// <summary>
        /// Called when your window is resized. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        /// <param name="e">Contains information on the new Width and Size of the GameWindow.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, this.Width / (float)this.Height, 0.1f, 10.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }
        #endregion

        #region OnRenderFrae
        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Color3(Color.White);
            GL.EnableClientState(EnableCap.ColorArray);

            #region Pass 1: Draw Object and pick Triangle
            GL.ClearColor(1f, 1f, 1f, 1f); // clears to uint.MaxValue
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.Translate(0f, 0f, -3f);
            GL.Rotate(angle, Vector3.UnitX);
            GL.Rotate(angle, Vector3.UnitY);
            angle += (float)e.Time * 3.0f;

            // You may re-enable the shader, but it works perfectly without and will run on intel HW too
             GL.UseProgram(ProgramObject);
            GL.DrawArrays(VBO_PrimMode, 0, VBO_Array.Length);
            // GL.UseProgram(0);

            // Read Pixel under mouse cursor
            Byte4 Pixel = new Byte4();
            GL.ReadPixels(Mouse.X, this.Height - Mouse.Y, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, ref Pixel);
            SelectedTriangle = Pixel.ToUInt32();
            #endregion Pass 1: Draw Object and pick Triangle

            GL.Color3(Color.White);
            GL.DisableClientState(EnableCap.ColorArray);

            #region Pass 2: Draw Shape
            if (SelectedTriangle == uint.MaxValue)
                GL.ClearColor(.2f, .1f, .3f, 1f); // purple
            else
                GL.ClearColor(0f, .2f, .3f, 1f); // cyan
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color3(1f, 1f, 1f);
            GL.DrawArrays(VBO_PrimMode, 0, VBO_Array.Length);

            GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
            GL.Color3(Color.Red);
            GL.DrawArrays(VBO_PrimMode, 0, VBO_Array.Length);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

            if (SelectedTriangle != uint.MaxValue)
            {
                GL.Disable(EnableCap.DepthTest);
                GL.Color3(Color.Green);
                GL.DrawArrays(VBO_PrimMode, (int)SelectedTriangle * 3, 3);
                GL.Enable(EnableCap.DepthTest);
            }
            #endregion Pass 2: Draw Shape

            this.SwapBuffers();

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Trace.WriteLine("Error at Swapbuffers: " + err);
        }
        #endregion

        protected override void Setup(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Resize(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}