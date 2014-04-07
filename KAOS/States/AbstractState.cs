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

        private double stateChangeDelay = 500;
        public int stateIndex;
        private String[] states = new String[] { "SceneGraphScene", "ModelScene", "SkyboxScene" };

        public void ProcessAutomaticDelay()
        {
            stateChangeDelay--;
            if (stateChangeDelay <= 0)
            {
                stateChangeDelay = 1000;
                stateIndex++;
                if (!(stateIndex + 1 > states.Length))
                {
                    m_stateManager.ChangeState(states[stateIndex]);
                }
            }
        }

        public abstract void Update(float elapsedTime, float aspect);
        public abstract void Render();

        public void Dispose()
        {
            m_textureManager.Dispose();
        }
    }
}
