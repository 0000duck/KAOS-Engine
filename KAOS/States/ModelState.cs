using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public class ModelState : AbstractState, IGameObject
    {
        public ModelState(StateManager stateManager)
        {
            m_bufferManager = new VertexBufferManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();
        }

        public void Update(float elapsedTime, float aspect)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }
    }
}