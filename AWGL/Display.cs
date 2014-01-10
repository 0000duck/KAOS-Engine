using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AWGL
{
    public class Display : GameWindow
    {
        private int vertexShaderID;
        private int fragmentShaderID;

        private int compiledShaderProgramID;

        public Display() : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {

        }   

        private void initProgram()
        {
            compiledShaderProgramID = GL.CreateProgram();
   
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

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
    }
}
