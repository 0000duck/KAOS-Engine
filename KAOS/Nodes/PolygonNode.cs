﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Nodes
{
    public class PolygonNode : Node
    {
        Vector3[] m_Verticies;
        Vector3 m_Normals, m_TexCoords;

        public PolygonNode()
        {
            m_Verticies = new Vector3[3];
            m_Normals = new Vector3();
        }
        public override void Render()
        {
            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < m_Verticies.Length; i++)
            {
                if (i < 1)
                {
                    GL.Normal3(m_Normals);
                }

                GL.Vertex3(m_Verticies[i]);
            }
            GL.End();
        }

        public void AddVertex(int index, Vector3 v)
        {
            m_Verticies[index] = v;
        }

        public void AddNormal(Vector3 n)
        {
            m_Normals = n;
        }

        public void AddTexCoord(Vector3 t)
        {

        }

    }
}
