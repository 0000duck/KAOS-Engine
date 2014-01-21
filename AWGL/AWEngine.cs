using AWGL.Scene;
using OpenTK;
using System;
using System.Drawing;

namespace AWGL
{
    public sealed class AWEngine
    {
        private static AWEngine instance = new AWEngine();

        private AWEngine()
        {
        }

        public static AWEngine getInstance()
        {
            return instance;
        }

        [STAThread]
        public static void Main()
        {
            using (ShaderTutorials game = new ShaderTutorials())
            {
                game.Run(30,0);
            }
        }


        public static string AppName
        {
            get
            {
                return "AWEngine";
            }
            
        }
    }
}
