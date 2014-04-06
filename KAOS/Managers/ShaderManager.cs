using KAOS.Utilities;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.IO;

namespace KAOS.Managers
{
    /// <summary>
    /// Responsible for building individual shaders and linking them to the main program.
    /// </summary>
    public static class ShaderManager
    {
        #region Class Fields
        private static Dictionary<string, Shader> m_shaderStorage;

        // Handles
        private static int m_vertexShaderHandle, m_fragmentShaderHandle, m_programHandle;

        private static string defaultDataPath = "Data/Shaders/";
        private static string m_vertexShaderFile = "skybox-vs";
        private static string m_fragmentShaderFile = "skybox-fs";
        #endregion

        #region Default Loaders
        internal static void LoadDefaultSkyboxShader()
        {
            if (m_shaderStorage == null)
                m_shaderStorage = new Dictionary<string, Shader>();
            m_programHandle = BuildProgram();
            m_shaderStorage.Add("skybox", new Shader(m_programHandle));
        }

        internal static void LoadDefaultRenderShader()
        {
            m_vertexShaderFile = "render-vs";
            m_fragmentShaderFile = "render-fs";
            if (m_shaderStorage == null)
                m_shaderStorage = new Dictionary<string, Shader>();
            m_programHandle = BuildProgram();
            m_shaderStorage.Add("render", new Shader(m_programHandle));
        }

        internal static void LoadDefaultAssimpShader()
        {
            m_vertexShaderFile = "assimp-vs";
            m_fragmentShaderFile = "assimp-fs";
            if (m_shaderStorage == null)
                m_shaderStorage = new Dictionary<string, Shader>();
            m_programHandle = BuildProgram();
            m_shaderStorage.Add("assimp", new Shader(m_programHandle));
        }
        #endregion

        public static void LoadCustomProgram(string shaderID, string vertexShaderPath, string fragmentShaderPath)
        {
            m_vertexShaderFile = vertexShaderPath;
            m_fragmentShaderFile = fragmentShaderPath;
            m_programHandle = BuildProgram();

            m_shaderStorage.Add(shaderID, new Shader(m_programHandle));
        }

        #region Shader and Program Contruction Methods
        internal static string LoadShader(string shaderSourcePath)
        {
            using (StreamReader sr = new StreamReader(defaultDataPath + shaderSourcePath + ".glsl"))
            {
                return sr.ReadToEnd();
            }
        }

        internal static int BuildShader(string shaderSourcePath, ShaderType shaderType)
        {
            // Create space in memory for the shader
            int shaderHandle = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderHandle, LoadShader(shaderSourcePath));

            // Compile
            GL.CompileShader(shaderHandle);

            Logger.ShaderInfo(shaderHandle);

            return shaderHandle;
        }

        internal static int BuildProgram()
        {
            m_vertexShaderHandle = BuildShader(m_vertexShaderFile, ShaderType.VertexShader);
            m_fragmentShaderHandle = BuildShader(m_fragmentShaderFile, ShaderType.FragmentShader);

            int programHandle = GL.CreateProgram();

            GL.AttachShader(programHandle, m_vertexShaderHandle);
            GL.AttachShader(programHandle, m_fragmentShaderHandle);

            GL.LinkProgram(programHandle);

            #region Check linker success
            int[] temp = new int[1];
            GL.GetProgram(programHandle, GetProgramParameterName.LinkStatus, out temp[0]);
            Logger.WriteLine("Linking Program (" + programHandle + ") " + ((temp[0] == 1) ? "succeeded." : "FAILED!"));
            #endregion

            #region Validate Program
            GL.ValidateProgram(programHandle);
            GL.GetProgram(programHandle, GetProgramParameterName.ValidateStatus, out temp[0]); // update to use OpenGL4
            Logger.WriteLine("Validating Program (" + programHandle + ") " + ((temp[0] == 1) ? "succeeded." : "FAILED!"));
            //if (validateSuccess == 0)
            //{
            //    String message;
            //    GL.GetProgramInfoLog(programHandle, out message);
            //    Logger.WriteLine("Program validation failed" + message);
            //}
            #endregion

            #region Registered Attributes
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveAttributes, out temp[0]);
            Logger.WriteLine("Program registered " + temp[0] + " Attributes.");

            Logger.WriteLine("End of Shader build. GL Error: " + GL.GetError());
            #endregion

            // Delete the shaders as the program has them now
            GL.DeleteShader(m_vertexShaderHandle);
            GL.DeleteShader(m_fragmentShaderHandle);

            return programHandle;
        }
        #endregion

        #region Public Accessors

        public static Shader Skybox
        {
            get
            {
                return Get("skybox");
            }
        }

        public static Shader Render
        {
            get
            {
                return Get("render");
            }
        }

        public static Shader Assimp
        {
            get
            {
                return Get("assimp");
            }
        }

        public static Shader Get(string shaderID)
        {
            return m_shaderStorage[shaderID];
        }

        #endregion
    }
}
