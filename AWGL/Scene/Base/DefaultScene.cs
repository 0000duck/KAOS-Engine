using AWGL.Shapes;
using AWGL.Shapes.Base;
using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AWGL.Scene.Base
{
    /// <summary>
    /// Controls Main Window functions and sets up OpenGL
    /// </summary>
    public abstract class DefaultScene : GameWindow
    {
        public DefaultScene()
            : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {
        }   

        #region Private Fields

        // GLSL Objects
        private int m_VertexShaderObject, m_FragmentShaderObject, m_ProgramObject, m_TextureObject;

        private int m_AttributeVertexPosition, m_AttributeVertexColor, m_UniformModelView;

        private int m_Position_VBO, m_Color_VBO, m_ModelView_VBO, m_Elements_IBO;

        private Vector3[] m_VertexData, m_ColorData;
        private List<Volume> m_Objects = new List<Volume>();
        private int[] m_IndiceData;

        private float m_Time = 0.0f;

        private Version m_Version, m_TargetLow, m_TargetHigh;

        #endregion Private Fields

        #region InitProgram
        
        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        private void InitProgram()
        {
            m_ProgramObject = GL.CreateProgram();

            loadShader("VS.glsl", ShaderType.VertexShader, m_ProgramObject, out m_VertexShaderObject);
            loadShader("FS.glsl", ShaderType.FragmentShader, m_ProgramObject, out m_FragmentShaderObject);

            // Links shaders and output any errors
            GL.LinkProgram(m_ProgramObject);
            Console.WriteLine(GL.GetProgramInfoLog(m_ProgramObject));

            // Get the values we need, and also do a simple check to make sure the attributes were found.
            m_AttributeVertexPosition = GL.GetAttribLocation(m_ProgramObject, "vPosition");
            m_AttributeVertexColor = GL.GetAttribLocation(m_ProgramObject, "vColor");
            m_UniformModelView = GL.GetUniformLocation(m_ProgramObject, "modelview");

            if (m_AttributeVertexPosition == -1 || m_AttributeVertexColor == -1 || m_UniformModelView == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            // This generates 4 separate buffers and stores their addresses in our variables. 
            // For multiple buffers like this, there's an option for generating multiple buffers 
            // and storing them in an array, but for simplicity's sake, we're keeping them in separate ints.
            GL.GenBuffers(1, out m_Position_VBO);
            GL.GenBuffers(1, out m_Color_VBO);
            GL.GenBuffers(1, out m_ModelView_VBO);
            GL.GenBuffers(1, out m_Elements_IBO);

            Random rand = new Random();

            float xPos = -1.0f;
            for (int i = 0; i < 2; i++)
            {
                Sierpinski sier = new Sierpinski();

                sier.Position = new Vector3(xPos, 0.0f, -2.5f);
                sier.Rotation = new Vector3(0.55f, 0.25f, 0);
                sier.Scale = Vector3.One;
                m_Objects.Add(sier);

                xPos = 1.0f;
            }
        }

        #endregion

        #region OnLoad

        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GetOpenGLVersion();

            InitProgram();

            Title = "AWGL: High level OpenTK wrapper   -   OpenGL " + m_Version.ToString();
            GL.ClearColor(Color.MidnightBlue);
            GL.PointSize(3f);
        }

        #endregion

        #region OnUnload

        protected override void OnUnload(EventArgs e)
        {

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
        }

        #endregion

        #region OnRenderFrame

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.EnableVertexAttribArray(m_AttributeVertexPosition);
            GL.EnableVertexAttribArray(m_AttributeVertexColor);

            int indiceAt = 0;
            
            foreach (Volume v in m_Objects)
            {
                GL.UniformMatrix4(m_UniformModelView, false, ref v.ModelViewProjectionMatrix);
                GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceAt*sizeof(uint));
                indiceAt += v.IndiceCount;
            }

            // Keep things clean:
            GL.DisableVertexAttribArray(m_AttributeVertexPosition);
            GL.DisableVertexAttribArray(m_AttributeVertexColor);
            GL.Flush();

            SwapBuffers();
        }

        #endregion

        #region OnUpdateFrame

        /// <summary>
        /// Add your game logic here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[OpenTK.Input.Key.Escape])
            {
                this.Exit();
            }

            m_Time += (float)e.Time;

            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();

            int vertCount = 0;

            foreach (Volume v in m_Objects)
            {
                verts.AddRange(v.GetVerts().ToList());
                inds.AddRange(v.GetIndices().ToList());
                colors.AddRange(v.GetColorData().ToList());
                vertCount += v.VertCount;
            }

            m_VertexData = verts.ToArray();
            m_IndiceData = inds.ToArray();
            m_ColorData = colors.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, m_Position_VBO);  // 1. Bind vertex data to the buffer.
            GL.BufferData<Vector3>(                                 // 2. Send data.
                BufferTarget.ArrayBuffer, (IntPtr)(m_VertexData.Length * Vector3.SizeInBytes), 
                m_VertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(                                 // 3. Tell OpenGL to use the last buffer bound to.
                m_AttributeVertexPosition, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, m_Color_VBO);     // 1. Bind color data to the buffer
            GL.BufferData<Vector3>(                                 // 2. Send Data
                BufferTarget.ArrayBuffer, (IntPtr)(m_ColorData.Length * Vector3.SizeInBytes),
                m_ColorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(                                 // 3. Tell OpenGL to use the last buffer bound to.
                m_AttributeVertexColor, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_Elements_IBO);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer, (IntPtr)(m_IndiceData.Length * sizeof(int)),
                m_IndiceData, BufferUsageHint.StaticDraw);
            
            // Rotate objects
            for (int i = 0; i < m_Objects.Count; i++)
            {
                m_Objects[i].Rotation = new Vector3(0.55f * m_Time, 0.25f * m_Time, 0);
            }

            // Send model view matrix
            foreach (Volume v in m_Objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = 
                    Matrix4.CreatePerspectiveFieldOfView(1.0f, ClientSize.Width / (float)ClientSize.Height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
            }

            GL.UseProgram(m_ProgramObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        #endregion

        #region GetOpenGLVersion

        /// <summary>
        /// Get OpenGL Version Information and check system meets requirements
        /// </summary>
        private void GetOpenGLVersion()
        {
            m_Version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            m_TargetLow = new Version(3, 1);
            m_TargetHigh = new Version(4, 1);
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

        #region loadShader

        /// <summary>
        /// Helper Funtion for loading shaders
        /// </summary>
        /// <param name="filename">Filename of GLSL Shader</param>
        /// <param name="type">Type of GLSL Shader to load</param>
        /// <param name="program">Program ID to add Shader too</param>
        /// <param name="address">Shader Pointer</param>
        private void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader("Shaders/" + filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        #endregion

    }
}
