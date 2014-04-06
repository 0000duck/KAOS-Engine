using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAOS.Interfaces;
using KAOS.Managers;

namespace Game.States
{
    class SplashScreen : IGameObject
    {
        double currentRotation = 0;
        double delay = 100;

        StateManager m_stateManager;
        public SplashScreen(StateManager stateManager)
        {
            m_stateManager = stateManager;
        }

        public void Update(float elapsedTime, float aspect)
        {
            delay--;
            if (delay <= 0){
                delay = 3;
                m_stateManager.ChangeState("SkyboxScene");
            }
            currentRotation = 10 * elapsedTime;
        }

        public void Render()
        {

        }
    }
}
