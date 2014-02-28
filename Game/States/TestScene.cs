using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAOS.Interfaces;
using KAOS.Managers;

namespace Game.States
{
    class TestScene : IGameObject
    {
        StateManager m_stateManager;
        public TestScene(StateManager stateManager)
        {
            m_stateManager = stateManager;
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
