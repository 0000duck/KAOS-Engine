using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace AWGL
{
    /// <summary>
    /// Responsible for building individual shaders and linking them to the main program.
    /// </summary>
    class AWShaderManager : IDisposable
    {
        /// <summary>
        /// Shader Pointers
        /// </summary>
        private int vShader, fShader, programHandle;

        private string defaultDataPath = "Data/Shaders/";
        private string m_vsFilePath, m_fsFilePath;

        public AWShaderManager(string vs_path, string fs_path)
        {
            this.m_vsFilePath = vs_path;
            this.m_fsFilePath = fs_path;
            BuildProgram();
        }

        public AWShaderManager()
        {
            this.m_vsFilePath   = "Simple_VS";
            this.m_fsFilePath   = "Simple_FS";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">Shader Filename</param>
        /// <returns>Shader Source Code</returns>
        private string LoadShader(string filename) 
        {
            using (StreamReader sr = new StreamReader(defaultDataPath + filename + ".glsl"))
            {
                return sr.ReadToEnd();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="shaderType"></param>
        /// <returns></returns>
        private int BuildShader(string filename, ShaderType shaderType)
        {
            // Create space in memory for the shader
            int shaderHandle = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderHandle, LoadShader(filename));

            // Compile
            GL.CompileShader(shaderHandle);

            // Check compile success
            int compileStatus;
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out compileStatus);

            if (compileStatus == 0)
            {
                String message;
                GL.GetShaderInfoLog(shaderHandle, out message);
                Debug.WriteLine("BuildShader failed to compile " + shaderType.ToString() + ": " + message);
                return -1;
            }

            return shaderHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildProgram() 
        {
            vShader = BuildShader(m_vsFilePath, ShaderType.VertexShader);
            fShader = BuildShader(m_fsFilePath, ShaderType.FragmentShader);

            Debug.WriteLine(GL.GetShaderInfoLog(vShader));
            Debug.WriteLine(GL.GetShaderInfoLog(fShader));

            programHandle = GL.CreateProgram();

            GL.AttachShader(programHandle, vShader);
            GL.AttachShader(programHandle, fShader);

            GL.LinkProgram(programHandle);

            #region Check linker success

            int linkSuccess;
            GL.GetProgram(this.programHandle, GetProgramParameterName.LinkStatus, out linkSuccess); // update to use OpenGL4
            if (linkSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.programHandle, out message);
                Debug.WriteLine("Program link failed: " + message);
            }

            #endregion

            #region Validate Program

            int validateSuccess;
            GL.ValidateProgram(this.programHandle);
            GL.GetProgram(this.programHandle, GetProgramParameterName.ValidateStatus, out validateSuccess); // update to use OpenGL4
            if (validateSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.programHandle, out message);
                Debug.WriteLine("Program validation failed", message);
            }
            #endregion

            // Delete the shaders as the program has them now
            GL.DeleteShader(vShader);
            GL.DeleteShader(fShader);
        }

        public void Dispose()
        {
            GL.DeleteProgram(this.programHandle);
        }

        public int ProgramHandle
        {
            get
            {
                return this.programHandle;
            }
        }
    }
}
