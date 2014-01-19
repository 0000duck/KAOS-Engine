using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL
{
    public class AWGroupNode : AWNode, IGroupNode, IEnumerable<ISceneNode>
    {
        double m_angle, m_rx, m_ry, m_rz;
        double m_tx, m_ty, m_tz;

        private IList<ISceneNode> m_children = new List<ISceneNode>();
        
        public AWGroupNode()
        {
            this.m_angle = 0;
            this.m_rx = 1;   //!!
            this.m_ry = 0;
            this.m_rz = 0;

            this.m_tx = 0;
            this.m_ty = 0;
            this.m_tz = 0;
        }

        public void SetRotation(double angle, double rx, double ry, double rz)
        {
            this.m_angle = angle;
            this.m_rx = rx;
            this.m_ry = ry;
            this.m_rz = rz;
        }

        public void SetTranslation(double tx, double ty, double tz)
        {
            this.m_tx = tx;
            this.m_ty = ty;
            this.m_tz = tz;
        }

        public override void Render()
        {
            GL.PushMatrix();
            GL.Translate(m_tx, m_ty, m_tz);
            if (m_angle != 0)
            {
                //GL.Rotate(m_angle, m_rx, m_ry, m_rz);
            }

            foreach (ISceneNode child in m_children)
            {
                child.Render();
            }
            GL.PopMatrix();
        }

        #region IEnumerator Implementation
        public IEnumerator<ISceneNode> GetEnumerator()
        {
            return m_children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_children.GetEnumerator();
        }
        #endregion ISceneNode Implementation

        #region IGroupNode Implementation

        public void AddChild(ISceneNode child)
        {
            m_children.Add(child);
        }

        public void RemoveChild(ISceneNode child)
        {
            m_children.Remove(child);
        }

        #endregion IGroupNode Implementation


    } 
}
