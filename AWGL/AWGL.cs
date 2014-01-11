using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using AWGL.Scene;

namespace AWGL
{
    public static class AWGL 
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Int32 Selection;

            Console.WriteLine("Please select a scene to load:");
            Console.WriteLine("");
            Console.WriteLine("1. Static VBO");
            Console.WriteLine("2. Dynamic VBO");
            Console.WriteLine("3. Texture 2D");
            Console.WriteLine("4. Anaylgraph Stereo");
            Console.WriteLine("5. FBO");
            Console.WriteLine("6. Picker");
            Int32.TryParse(Console.ReadLine(), out Selection);

            switch (Selection)
            {
                case 1:
                    using (StaticVBOScene scene = new StaticVBOScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 2:
                    using (DynamicVBOScene scene = new DynamicVBOScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 3:
                    using (Texture2DScene scene = new Texture2DScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 4:
                    using (StereoVisionScene scene = new StereoVisionScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 5:
                    using (FBOScene scene = new FBOScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 6:
                    using (PickerScene scene = new PickerScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
            }
        }

    }
}
