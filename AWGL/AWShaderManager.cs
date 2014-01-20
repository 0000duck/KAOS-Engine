using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL;

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
            this.vShader = BuildShader("Simple_VS", ShaderType.VertexShader);
            this.fShader = BuildShader("Simple_FS", ShaderType.FragmentShader);

            this.linkedProgram = GL.CreateProgram();
            GL.AttachShader(linkedProgram, vShader);
            GL.AttachShader(linkedProgram, fShader);
            GL.LinkProgram(linkedProgram);

            // Check linker success
            int linkSuccess;
            GL.GetProgram(this.linkedProgram, ProgramParameter.LinkStatus, out linkSuccess); // update to use OpenGL4
            if (linkSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.linkedProgram, out message);
                Console.WriteLine("Program link failed: " + message);
            }

            // Validate program
            int validateSuccess;
            GL.ValidateProgram(this.linkedProgram);
            GL.GetProgram(this.linkedProgram, ProgramParameter.ValidateStatus, out validateSuccess); // update to use OpenGL4
            if (validateSuccess == 0)
            {
                String message;
                GL.GetProgramInfoLog(this.linkedProgram, out message);
                Console.WriteLine("Program validation failed", message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int programID() 
        {
            if(linkedProgram == null)
                BuildProgram();

            return linkedProgram;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
