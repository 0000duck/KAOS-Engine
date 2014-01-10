using OpenTK;

namespace AWGL.Shapes
{
    class ColourCube : Cube
    {
        Vector3 Color = new Vector3(1, 1, 1);
 
        public ColourCube(Vector3 color) : base() {
            Color = color;
        }

        public ColourCube()
        {

        }

        public override Vector3[] GetColorData()
        {
            return new Vector3[] { 
                Color,
                Color, 
                Color,
                Color,
                Color, 
                Color, 
                Color, 
                Color
            };
        }
    }
}
