using AWGL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL.Nodes
{
    public abstract class AWNode
    {
        protected BufferManager m_BufferManager;

        protected AWNode()
        {
            //m_BufferManager = new AWBufferManager();
        }

        public abstract void Render();
    }

}
