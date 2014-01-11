using OpenTK;
namespace AWGL
{
    public struct Vbo
    {
        public int VboID, EboID, NumElements;
    }
    
    // this struct is used for drawing
    public struct VertexC4ubV3f
    {
        public byte R, G, B, A;
        public Vector3 Position;

        public static int SizeInBytes = 16;
    }

    // this struct is used for updates
    public struct ParticleAttribut
    {
        public Vector3 Direction;
        public uint Age;

        //  more stuff could be here: Rotation, Radius, whatever
    }
}