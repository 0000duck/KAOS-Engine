using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    public struct VertexBuffer
    {
        #region Public Buffer Pointer IDs

        public int VaoID { get; set; }

        public int VboID { get; set; }

        public int IboID { get; set; }

        public PrimitiveType PrimitiveType { get; set; }

        public Vector3[] PositionData { get; set; }

        public Vector3[] NormalsData { get; set; }

        public Color4[] ColorData { get; set; }

        public uint[] IndicesData { get; set; }

        #endregion

        public VertexBuffer(int vaoId, int vboId, int iboId, int vPosition, int vNormals, int vColor, 
            Vector3[] vPositionData, Vector3[] vNormalsData, Color4[] vColorData, PrimitiveType primitiveType, uint[] indicesData) :this()
        {
            #region Buffer Pointer IDs
            VaoID = vaoId;
            VboID = vboId;
            IboID = iboId;
            #endregion
            
            PositionData = vPositionData;
            NormalsData = vNormalsData;
            ColorData = vColorData;
            IndicesData = indicesData;
        }

    }
}
