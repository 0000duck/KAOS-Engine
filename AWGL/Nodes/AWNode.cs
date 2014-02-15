using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.Nodes
{
    public abstract class AWNode
    {
        protected BufferObjectManager m_BufferManager;

        protected AWNode()
        {
            //m_BufferManager = new AWBufferManager();
        }

        public abstract void Render();
    }

}
