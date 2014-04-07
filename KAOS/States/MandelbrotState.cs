using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public class MandelbrotState : AbstractState
    {
        #region Contructors

        public MandelbrotState(StateManager stateManager)
        {
            m_bufferManager = new VertexBufferManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();
        }

        #endregion

        public override void Update(float elapsedTime, float aspect)
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
