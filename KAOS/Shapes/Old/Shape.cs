#region --- License ---
/* Copyright (c) 2006, 2007 Stefanos Apostolopoulos
 * See license.txt for license info
 */
#endregion



using KAOS.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace KAOS.Shapes
{
    public abstract class Shape : IDrawableShape
    {
        protected PrimitiveType PrimitiveMode;
        
        private Vector3[] vertices, normals;
        private Vector2[] texcoords;
        private uint[] indices;
        private int[] colors;

        public Vector3[] Vertices
        {
            get { return vertices; }
            protected set { vertices = value; }
        }

        public Vector3[] Normals
        {
            get { return normals; }
            protected set { normals = value; }
        }

        public Vector2[] Texcoords
        {
            get { return texcoords; }
            protected set { texcoords = value; }
        }

        public uint[] Indices
        {
            get { return indices; }
            protected set { indices = value; }
        }

        public int[] Colors
        {
            get { return colors; }
            protected set
            {
                colors = value;
            }
        }

        public void GetArraysforVbo(out PrimitiveType primitives, out VertexT2dN3dV3d[] vertices, out uint[] indices)
        {
            primitives = PrimitiveMode;

            vertices = new VertexT2dN3dV3d[Vertices.Length];
            for (uint i = 0; i < Vertices.Length ; i++)
            {
                //vertices[i].TexCoord = (Vector2d)Texcoords[i];
                vertices[i].Normal = (Vector3d)Normals[i] ;
                vertices[i].Position = (Vector3d)Vertices[i];
            }

            indices = Indices;
        }

        public void GetArraysforVbo(out PrimitiveType primitives, out VertexT2fN3fV3f[] vertices, out uint[] indices)
        {
            primitives = PrimitiveMode;

            vertices = new VertexT2fN3fV3f[Vertices.Length];
            for (uint i = 0; i < Vertices.Length; i++)
            {
                //vertices[i].TexCoord = Texcoords[i];
                vertices[i].Normal = Normals[i];
                vertices[i].Position = Vertices[i];
            }

            indices = Indices;
        }

        public void GetArraysforVbo(out PrimitiveType primitives, out VertexT2hN3hV3h[] vertices, out uint[] indices)
        {
            primitives = PrimitiveMode;

            vertices = new VertexT2hN3hV3h[Vertices.Length];
            for (uint i = 0; i < Vertices.Length; i++)
            {
                //vertices[i].TexCoord = (Vector2h)Texcoords[i];
                vertices[i].Normal = (Vector3h)Normals[i];
                vertices[i].Position = (Vector3h) Vertices[i];
            }

            indices = Indices;
        }
    }
}
