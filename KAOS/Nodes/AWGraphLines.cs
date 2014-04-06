using KAOS.Interfaces;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Nodes
{
    class AWGraphLines : AWNode
    {
        private IList<ISceneNode> m_children = new List<ISceneNode>();

        public override void Render()
        {
            GL.Color3(.3d, .3f, .3f);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(.0f, -.001f, .0f);
            GL.Vertex3(.0f, -.001f, 10.0f);
            GL.Vertex3(10.0f, -.001f, 10.0f);
            GL.Vertex3(10.0f, -.001f, .0f);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i <= 10; i++)
            {
                if (i == 0) { GL.Color3(.6f, .3f, .3f); } else { GL.Color3(.25f, .25f, .25f); }
                GL.Vertex3((float)i, .0f, .0f);
                GL.Vertex3((float)i, .0f, 10.0f);
                if (i == 0) { GL.Color3(.3f, .3f, .6f); } else { GL.Color3(.25f, .25f, .25f); }
                GL.Vertex3(.0f, .0f, (float)i);
                GL.Vertex3(10.0f, .0f, (float)i);
            }
            GL.End();
        }

    }
}
