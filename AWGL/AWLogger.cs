using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AWGL
{
    /// <summary>
    /// AWLogger
    /// </summary>
    internal static class AWLogger
    {
       
        internal static void WriteLine(string output)
        {
            Debug.WriteLine(AWEngine.AppName + " Logger: " + output.Trim());
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
