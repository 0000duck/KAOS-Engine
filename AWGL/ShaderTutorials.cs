using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
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
        static double elapsedSeconds;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = AWUtils.PrintOpenGLInfo();
            elapsedSeconds = 0;

            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            timer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            elapsedSeconds++;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            float[] color = new float[] 
            { 
                (float)(Math.Sin(elapsedSeconds) * 0.5f + 0.5f), 
                (float)(Math.Cos(elapsedSeconds) * 0.5f + 0.5f),
                0.0f, 
                1.0f 
            };

            GL.ClearBuffer(ClearBuffer.Color, 0, color);

            SwapBuffers();
        }
    }
}
