using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public abstract class AbstractState : IDisposable, IGameObject
    {
        protected VertexBufferManager m_bufferManager;
        protected StateManager m_stateManager;
        protected TextureManager m_textureManager;

        public abstract void Update(float elapsedTime, float aspect);
        public abstract void Render();

        public void Dispose()
        {
            m_textureManager.Dispose();
        }
    }
}
