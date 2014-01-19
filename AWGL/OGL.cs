using AWGL.Scene;
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

        public static void SetupScene(Color backgroundColor)
        {
        }

        [STAThread]
        public static void Run()
        {
            Int32 Selection;
            
            Console.WriteLine("Please select a scene to load:\n");

            Console.WriteLine("1. Dynamic VBO");
            Console.WriteLine("2. Texture 2D");
            Console.WriteLine("3. Anaylgraph Stereo");
            Console.WriteLine("4. FBO");
            Console.WriteLine("5. Picker");
            Console.WriteLine("6. Stencil CSG");
            Console.WriteLine("7. Scene Graph Test\n");
            Int32.TryParse(Console.ReadLine(), out Selection);

            switch (Selection)
            {
                case 2:
                    using (Texture2DScene scene = new Texture2DScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 3:
                    using (StereoVisionScene scene = new StereoVisionScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 4:
                    using (FBOScene scene = new FBOScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 5:
                    using (PickerScene scene = new PickerScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 6:
                    using (StencilCSGScene scene = new StencilCSGScene())
                    {
                        scene.Run(30.0);
                    }
                    break;
                case 7:
                    using (SceneGraphTest scene = new SceneGraphTest())
                    {
                        scene.Run(30.0);
                    }
                    break;
            }
        }
        
    }
}
