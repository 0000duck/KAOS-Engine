using AWGL.Scene;
using OpenTK;
using System;
using System.Drawing;

namespace AWGL
{
    public sealed class OGL
    {
        private static OGL instance = new OGL();

        private OGL()
        {
        }

        public static OGL getInstance()
        {
            return instance;
        }

        [STAThread]
        public static void Run()
        {
            using (DefaultScene game = new DefaultScene())
            {
                game.Run(30,0);
            }
        }
        
    }
}
