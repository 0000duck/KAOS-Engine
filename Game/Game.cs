using Game.States;
using KAOS;
using KAOS.Managers;
using KAOS.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Game : KAOSEngine
    {
        public StateManager stateManager = new StateManager();

        public Game(int width, int height, int major, int minor) : base(width, height, major, minor) { }

        public override void Initialise()
        {
            SetupStates();
            SetState("SplashScreen");
        }

        private void SetupStates()
        {
            stateManager.AddState("SplashScreen", new SplashScreen(stateManager));
            stateManager.AddState("TestScene", new Skyboxstate(stateManager));
        }

        private void SetState(string stateToLoad)
        {
            stateManager.ChangeState(stateToLoad);
        }

        public override void UpdateFrame(float elapsedTime)
        {
            stateManager.Update(elapsedTime, (float)ScreenWidth / ScreenHeight);
        }

        public override void RenderFrame(float elapsedTime)
        {
            stateManager.Render();
        }

    }


}
