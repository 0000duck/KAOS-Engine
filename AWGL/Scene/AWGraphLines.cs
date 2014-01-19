using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL
{
    class AWGraphLines : AWNode
    {
        private int m_gridSize = 20;

        public override void Render()
        {
            GL.Begin(PrimitiveType.Lines);
            for (int i = -m_gridSize; i <= m_gridSize; i++)
            {
                if (i == 0) { GL.Color3(.6f, .3f, .3f); } else { GL.Color3(Color.LightGray); }
                GL.Vertex3((float)i, .0f, -(float)m_gridSize);
                GL.Vertex3((float)i, .0f, (float)m_gridSize);
                if (i == 0) { GL.Color3(.3f, .3f, .6f); } else { GL.Color3(Color.LightGray); }
                GL.Vertex3(-(float)m_gridSize, .0f, (float)i);
                GL.Vertex3((float)m_gridSize, .0f, (float)i);
            }
            GL.End();
        }

    }
}
