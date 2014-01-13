using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL
{
    public class AWPolygon : AWNode
    {
        Vector3[] m_Verticies, m_Normals, m_TexCoords;

        public override void Render()
        {
            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < m_Verticies.Length; i++)
            {
                if(i < m_Normals.Length){
                    GL.Normal3(m_Normals[i]);
                }

                GL.Vertex3(m_Verticies[i]);
            }
            GL.End();
        }

        public void AddVertex(Vector3 v) 
        {
            m_Verticies.SetValue(v, m_Verticies.Length + 1);
        }

        public void AddNormal(Vector3 n)
        {
            m_Normals.SetValue(n, m_Normals.Length + 1);
        }

        public void AddTexCoord(Vector3 t)
        {

        }

    }
}
