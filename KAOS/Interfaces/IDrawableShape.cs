using KAOS.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Interfaces
{
    public interface IDrawableShape
    {
        Vector3[] Vertices { get; }
        Vector3[] Normals { get; }
        Vector2[] Texcoords { get; }
        uint[] Indices { get; }

        void GetArraysforVBO(out BeginMode primitives, out VertexT2dN3dV3d[] vertices, out uint[] indices);
        void GetArraysforVBO(out BeginMode primitives, out VertexT2fN3fV3f[] vertices, out uint[] indices);
        void GetArraysforVBO(out BeginMode primitives, out VertexT2hN3hV3h[] vertices, out uint[] indices);
    }
}
