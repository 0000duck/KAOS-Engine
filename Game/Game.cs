using AWGL;
using AWGL.Managers;
using AWGL.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Game : AWEngineWindow
    {
        public StateManager stateManager = new StateManager();

        public Game(int width, int height, int major, int minor) : base(width, height, major, minor) { }

        public override void Initialise()
        {
            SetupStates();
            stateManager.SetState("skybox");
        }

        private void SetupStates()
        {
            stateManager.AddState("skybox", new Skyboxstate(stateManager));
            stateManager.AddState("vbo", new VboState(stateManager));
        }

        private void SetState(string stateToLoad)
        {
            stateManager.SetState(stateToLoad);
        }

        public override void UpdateFrame(float elapsedTime)
        {
            stateManager.Update(elapsedTime);
        }

        public override void RenderFrame(float elapsedTime)
        {
            stateManager.Render();
        }

    }


}
