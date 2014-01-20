using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL4;

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
        private int vShader, fShader, linkedProgram;

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
                Console.WriteLine("BuildShader failed to compile " + shaderType.ToString() + ": " + message);
                return -1;
            }

            return shaderHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildProgram() 
        {
            this.vShader = BuildShader(m_vsFilePath, ShaderType.VertexShader);
            this.fShader = BuildShader(m_fsFilePath, ShaderType.FragmentShader);

            this.linkedProgram = GL.CreateProgram();
            GL.AttachShader(linkedProgram, vShader);
            GL.AttachShader(linkedProgram, fShader);
            GL.LinkProgram(linkedProgram);

            // Check linker success
            int linkSuccess;
            GL.GetProgram(this.linkedProgram, GetProgramParameterName.LinkStatus, out linkSuccess); // update to use OpenGL4
            if (linkSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.linkedProgram, out message);
                Console.WriteLine("Program link failed: " + message);
            }

            // Validate program
            int validateSuccess;
            GL.ValidateProgram(this.linkedProgram);
            GL.GetProgram(this.linkedProgram, GetProgramParameterName.ValidateStatus, out validateSuccess); // update to use OpenGL4
            if (validateSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.linkedProgram, out message);
                Console.WriteLine("Program validation failed", message);
            }

            // Delete the shaders as the program has them now
            GL.DeleteShader(vShader);
            GL.DeleteShader(fShader);
        }

        public void Dispose()
        {
            GL.DeleteProgram(this.linkedProgram);
        }

        public int ProgramID
        {
            get
            {
                return this.linkedProgram;
            }
        }
    }
}
