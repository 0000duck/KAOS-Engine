using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;

namespace AWGL.Utilities
{
    /// <summary>
    /// AWLogger
    /// </summary>
    internal static class Logger
    {
       
        internal static void WriteLine(string output)
        {
            Console.WriteLine(KAOSEngine.AppName + " Logger: " + output.Trim());
        }

        internal static void PlatformInfo()
        {
            WriteLine("Starting Logger. . .");
            WriteLine("Getting Platform Information. . .");
            WriteLine(GL.GetString(StringName.Vendor));
            WriteLine(GL.GetString(StringName.Renderer));
            WriteLine(GL.GetString(StringName.Version));
            WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
        }

        internal static void ShaderInfo(int shaderHandle)
        {
            String infoLog;
            GL.GetShaderInfoLog(shaderHandle, out infoLog);
            WriteLine(infoLog);
        }

        internal static void ProgramInfo(int programHandle)
        {
            String infoLog;
            GL.GetProgramInfoLog(programHandle, out infoLog);
            WriteLine(infoLog);
            ShadersAttached(programHandle);
        }

        internal static void ShadersAttached(int programHandle)
        {
            int attachedShaders;
            GL.GetProgram(programHandle, GetProgramParameterName.AttachedShaders, out attachedShaders);
            string temp = attachedShaders > 1 ? " Shaders" : " Shader";
            WriteLine(attachedShaders + temp + " Attached");
        }
    }

}
