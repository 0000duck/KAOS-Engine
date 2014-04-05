using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public class MandelbrotState : AbstractState, IGameObject
    {
        #region Contructors

        public MandelbrotState(StateManager stateManager)
        {
            m_bufferManager = new VertexBufferManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();

            LoadQuad();
        }

        #endregion

        #region IGameObject Implementation

        public void Update(float elapsedTime, float aspect)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
