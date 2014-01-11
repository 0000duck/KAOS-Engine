using OpenTK;
using System;
namespace AWGL
{
    public struct Vbo
    {
        public int VboID, EboID, NumElements;
    }

    #region Particles
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
    #endregion

    #region Picker
    public struct Byte4
    {
        public byte R, G, B, A;

        public Byte4(byte[] input)
        {
            R = input[0];
            G = input[1];
            B = input[2];
            A = input[3];
        }

        public uint ToUInt32()
        {
            byte[] temp = new byte[] { this.R, this.G, this.B, this.A };
            return BitConverter.ToUInt32(temp, 0);
        }

        public override string ToString()
        {
            return this.R + ", " + this.G + ", " + this.B + ", " + this.A;
        }
    }

    struct Vertex
    {
        public Byte4 Color; // 4 bytes
        public Vector3 Position; // 12 bytes

        public const byte SizeInBytes = 16;
    }
    #endregion

}