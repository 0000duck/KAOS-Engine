using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AWGL
{
    public class ShaderTutorials : GameWindow
    {
        static Timer timer;
        static double elapsedSeconds = 0;
        static AWShaderManager shaderManager;
        private int programObject, vertexObject, fragmentObject, VAO;

        public ShaderTutorials()
            : base(800, 600, GraphicsMode.Default, "HI", GameWindowFlags.Default,
            DisplayDevice.Default, 2, 0, GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);

            // Shaders
            shaderManager = new AWShaderManager("CH02_VS", "CH02_FS");
            //vertexObject = AWUtils.BuildShader("CH02_VS", ShaderType.VertexShader);

            int attachedShaders;
            GL.GetProgram(shaderManager.ProgramID, GetProgramParameterName.AttachedShaders, out attachedShaders);

            Console.WriteLine("Attached Shaders: " + attachedShaders);

            // VAO Setup
            GL.GenVertexArrays(1, out VAO);
            GL.BindVertexArray(VAO);
            

            Title = AWUtils.PrintOpenGLInfo();
            
            // Timer Setup
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            timer.Enabled = true;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.DeleteBuffer(VAO);
            
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            elapsedSeconds++;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //base.OnRenderFrame(e);

            Color4 color = new Color4(0.0f, 0.0f, 0.2f, 1.0f);            
            
            GL.ClearColor(color);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Use program object created by the shaderManager
            GL.UseProgram(shaderManager.ProgramID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            ErrorCode err;
            
            err = GL.GetError();

            SwapBuffers();
        }

    }
}
