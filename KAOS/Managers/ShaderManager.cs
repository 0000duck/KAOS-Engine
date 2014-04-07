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
        private static Dictionary<string, Shader> _shaders;

        // Handles
        private static int vertexShaderHandle, _fragmentShaderHandle, _programHandle;

        private const string DefaultDataPath = "Data/Shaders/";
        private static string _vertexShaderFile = "skybox-vs";
        private static string _fragmentShaderFile = "skybox-fs";
        #endregion

        #region Default Loaders
        internal static void LoadDefaultSkyboxShader()
        {
            if (_shaders == null)
                _shaders = new Dictionary<string, Shader>();
            _programHandle = BuildProgram();
            _shaders.Add("skybox", new Shader(_programHandle));
        }

        internal static void LoadDefaultRenderShader()
        {
            _vertexShaderFile = "render-vs";
            _fragmentShaderFile = "render-fs";
            if (_shaders == null)
                _shaders = new Dictionary<string, Shader>();
            _programHandle = BuildProgram();
            _shaders.Add("render", new Shader(_programHandle));
        }

        internal static void LoadDefaultAssimpShader()
        {
            _vertexShaderFile = "assimp-vs";
            _fragmentShaderFile = "assimp-fs";
            if (_shaders == null)
                _shaders = new Dictionary<string, Shader>();
            _programHandle = BuildProgram();
            _shaders.Add("assimp", new Shader(_programHandle));
        }
        #endregion

        public static void LoadCustomProgram(string shaderID, string vertexShaderPath, string fragmentShaderPath)
        {
            _vertexShaderFile = vertexShaderPath;
            _fragmentShaderFile = fragmentShaderPath;
            _programHandle = BuildProgram();

            _shaders.Add(shaderID, new Shader(_programHandle));
        }

        #region Shader and Program Contruction Methods
        internal static string LoadShader(string shaderSourcePath)
        {
            using (StreamReader sr = new StreamReader(DefaultDataPath + shaderSourcePath + ".glsl"))
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
            vertexShaderHandle = BuildShader(_vertexShaderFile, ShaderType.VertexShader);
            _fragmentShaderHandle = BuildShader(_fragmentShaderFile, ShaderType.FragmentShader);

            int programHandle = GL.CreateProgram();

            //GL.AttachShader(programHandle, vertexShaderHandle);
            GL.AttachShader(programHandle, _fragmentShaderHandle);

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
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveUniforms, out temp[0]);
            Logger.WriteLine("Program registered " + temp[0] + " Uniforms.");

            Logger.WriteLine("End of Shader build. GL Error: " + GL.GetError());
            #endregion

            // Delete the shaders as the program has them now
            //GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(_fragmentShaderHandle);

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
            return _shaders[shaderID];
        }

        #endregion
    }
}
