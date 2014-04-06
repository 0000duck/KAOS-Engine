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
        protected double delay = 1000;
        protected int stateIndex;
        protected String[] states = new String[]
        {
            "SceneGraphScene",
            "ModelScene",
            "SkyboxScene"
        };

        public void Dispose()
        {
            m_textureManager.Dispose();
        }

        protected void ProcessAutomaticDelay()
        {
            delay--;
            if (delay <= 0)
            {
                
                delay = 1000;
                stateIndex++;
                m_stateManager.ChangeState(states[stateIndex]);
            }
        }

        public abstract void Update(float elapsedTime, float aspect);
        public abstract void Render();
    }
}
