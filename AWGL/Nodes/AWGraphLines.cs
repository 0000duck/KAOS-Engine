using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL.Nodes
{
    class AWGraphLines : AWNode, ISceneNode
    {
        private int m_gridSize = 20;

        public Vector3[] Vertices
        {
            get { return m_vertices; }
        }

        public int[] Indices
        {
            get { throw new NotImplementedException(); }
        }

        private static Vector3[] m_vertices;

        public AWGraphLines(int gridSize)
        {
            m_gridSize = gridSize;
            m_vertices = new Vector3[m_gridSize];
            BuildVertices();
        }

        private void BuildVertices()
        {
            for (int i = 0; i < m_gridSize; i += 4)
            {
                m_vertices[i] = new Vector3((float)i, .0f, -(float)m_gridSize);
                m_vertices[i + 1] = new Vector3((float)i, .0f, (float)m_gridSize);
                m_vertices[i + 2] = new Vector3(-(float)m_gridSize, .0f, (float)i);
                m_vertices[i + 3] = new Vector3((float)m_gridSize, .0f, (float)i);
            }
        }

        public override void Render()
        {

        }

    }
}
