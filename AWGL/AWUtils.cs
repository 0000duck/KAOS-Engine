using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace AWGL
{
    /// <summary>
    /// Utility functions
    /// </summary>
    public class AWUtils
    {
        /// <summary>
        /// Helper Funtion for loading shaders. Returns Shader Source from file.
        /// </summary>
        /// <param name="filename">Filename of GLSL Shader</param>
        /// <returns>Shader Source Code</returns>
        public static string LoadShader(String filename)
        {
            string dataPath = "Data/Shaders/";
            string shaderSource;

            using (StreamReader sr = new StreamReader(dataPath + filename))
            {
                shaderSource = sr.ReadToEnd();
            }

            return shaderSource;
        }     
        
        /// <summary>
        /// Load the shader file, creates an OpenGL shader object, compiles the 
        /// source code and returns the handle to the internal shader object. 
        /// If the compilation fails, the application will exit.
        /// </summary>
        /// <param name="filename">Filename of GLSL Shader</param>
        /// <param name="type">Type of GLSL Shader to load</param>
        /// <returns>Shader Handle</returns>
        public static int BuildShader(string filename, ShaderType shaderType)
        {
            string shaderSource = LoadShader(filename);

            int shaderHandle = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderHandle, shaderSource);
            GL.CompileShader(shaderHandle);

            // Check compile success
            int compileStatus;
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out compileStatus);

            if (compileStatus == 0)
            {
                String message;
                GL.GetShaderInfoLog(shaderHandle, out message);
                Console.WriteLine("BuildShader failed to compile " + shaderType.ToString() + ": " + message);
                return -1;
            }

            return shaderHandle;
        }

        /// <summary>
        /// Creates a program object, attaches the shaders, links them and 
        /// returns the OpenGL handle of the program.
        /// </summary>
        /// <param name="vertexShaderId">Shader Handle</param>
        /// <param name="fragmentShaderId">Shader Handle</param>
        /// <returns>Shader Program Handle</returns>
        public static int BuildProgram(int vertexShaderId, int fragmentShaderId)
        {
            int programHandle = GL.CreateProgram();
            GL.AttachShader(programHandle, vertexShaderId);
            GL.AttachShader(programHandle, fragmentShaderId);
            GL.LinkProgram(programHandle);

            // Check linker success
            int linkSuccess;
            GL.GetProgram(programHandle, ProgramParameter.LinkStatus, out linkSuccess);
            if (linkSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(programHandle, out message);
                Console.WriteLine("Program link failed: " + message);
            }

            // Validate program
            int validateSuccess;
            GL.ValidateProgram(programHandle);
            GL.GetProgram(programHandle, ProgramParameter.ValidateStatus, out validateSuccess);
            if (validateSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(programHandle, out message);
                Console.WriteLine("Program validation failed", message);
            }

            return programHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="vertices"></param>
        /// <param name="elements"></param>
        /// <param name="elementSize"></param>
        /// <param name="typeSize"></param>
        /// <param name="bufferUsageTypeGL"></param>
        /// <returns></returns>
        public static Vbo LoadVBO<TVertex>(TVertex[] vertices,
                                           short[] elements, 
                                           int elementSize, 
                                           int typeSize, 
                                           BufferUsageHint bufferUsageTypeGL) 
            where TVertex : struct
        {
            Vbo vboHandle = new Vbo();

            vboHandle.NumElements = elements.Length;

            // Determine size of Buffer
            int vbo_Size = vertices.Length * BlittableValueType.StrideOf(vertices);
            int ebo_Size = elements.Length * sizeof(short);

            #region
            // To create a VBO:
            // 1) Generate the buffer handles for the vertex and element buffers.
            // 2) Bind the vertex buffer handle and upload your vertex data. 
            //    Check that the buffer was uploaded correctly.
            // 3) Bind the element buffer handle and upload your element data. 
            //    Check that the buffer was uploaded correctly.
            #endregion

            //Generate Buffer ID
            GL.GenBuffers(1, out vboHandle.VboID);

            // Binds the buffer that is used next
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle.VboID);

            // Copy data to the VBO on the GPU.
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)vbo_Size, vertices, bufferUsageTypeGL);

            int getBufferSize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out getBufferSize);
            if (getBufferSize != vbo_Size)
                throw new Exception("Vertex data not uploaded correctly");

            GL.GenBuffers(1, out vboHandle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboHandle.EboID);

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)ebo_Size, elements, bufferUsageTypeGL);

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out getBufferSize);
            if (getBufferSize != ebo_Size)
                throw new Exception("Element data not uploaded correctly");

            return vboHandle;
        }

        #region TestOpenGLVersion
        /// <summary>
        /// Get OpenGL Version Information and check system meets requirements
        /// </summary>
        public static void TestOpenGLVersion()
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

        public static string PrintOpenGLInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("Video informations :");
            Console.WriteLine("Graphics card vendor : {0}", GL.GetString(StringName.Vendor));
            Console.WriteLine("Renderer : {0}", GL.GetString(StringName.Renderer));
            Console.WriteLine("Version : {0}", GL.GetString(StringName.Version));
            Console.WriteLine("Shading Language Version : {0}", GL.GetString(StringName.ShadingLanguageVersion));
            
            TestOpenGLVersion();

            return "AWGL Engine Prototype      - " + GL.GetString(StringName.Renderer) + " (GL " + GL.GetString(StringName.Version) + ")";
        }
    
    }
}