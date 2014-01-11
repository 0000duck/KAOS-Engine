using AWGL.Shapes;
using AWGL.Tutorial;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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
            this.WindowState = WindowState.Fullscreen;
            Keyboard.KeyDown += Keyboard_KeyDown;
        }

        protected ErrorCode err;

        #region OnLoad
        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            TestOpenGLVersion();

            Title = "AWGL: High level OpenTK wrapper - " + GL.GetString(StringName.Renderer) + " (GL " + GL.GetString(StringName.Version) + ")";

            GL.ClearColor(.1f, 0f, .1f, 0f);         
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
        }
        #endregion

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

        #region LoadShader(String filename, ShaderType type, int program, out int address)
        /// <summary>
        /// Helper Funtion for loading shaders
        /// </summary>
        /// <param name="filename">Filename of GLSL Shader</param>
        /// <param name="type">Type of GLSL Shader to load</param>
        /// <param name="program">Program ID to add Shader too</param>
        /// <param name="address">Shader Pointer</param>
        protected void LoadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            string sType = "";

            switch (type)
            {
                case ShaderType.VertexShader:
                    sType = "Vertex ";
                    break;
                case ShaderType.FragmentShader:
                    sType = "Fragment ";
                    break;
            }

            using (StreamReader sr = new StreamReader("Data/Shaders/" + filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
                GL.CompileShader(address);
            }

            err = GL.GetError();
            if (err != ErrorCode.NoError)
                Trace.WriteLine(sType + "Shader: " + err);

            string LogInfo;
            GL.GetShaderInfoLog(address, out LogInfo);
            if (LogInfo.Length > 0 && !LogInfo.Contains("hardware"))
            {
                Trace.WriteLine(sType + "Shader failed!\nLog:\n" + LogInfo);
            }
            else
            {
                Trace.WriteLine(sType + "Shader compiled without complaint.");
                GL.AttachShader(program, address);
            }
        }

        #endregion

        #region LoadVBO<TVertex> (TVertex[] vertices, short[] elements) where TVertex : struct
        protected Vbo LoadVBO<TVertex>(TVertex[] vertices, short[] elements) where TVertex : struct
        {
            Vbo handle = new Vbo();
            int size;

            // To create a VBO:
            // 1) Generate the buffer handles for the vertex and element buffers.
            // 2) Bind the vertex buffer handle and upload your vertex data. 
            //    Check that the buffer was uploaded correctly.
            // 3) Bind the element buffer handle and upload your element data. 
            //    Check that the buffer was uploaded correctly.

            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * BlittableValueType.StrideOf(vertices)), vertices,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * BlittableValueType.StrideOf(vertices) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * sizeof(short)), elements,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (elements.Length * sizeof(short) != size)
                throw new ApplicationException("Element data not uploaded correctly");

            handle.NumElements = elements.Length;
            return handle;
        }

        #endregion

        #region Keyboard_KeyDown
        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <param name="sender">The KeyboardDevice which generated this event.</param>
        /// <param name="e">The key that was pressed.</param>
        protected void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }
        #endregion

    }
}
