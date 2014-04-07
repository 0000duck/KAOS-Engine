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
        public Game(int width, int height, int major, int minor) : base(width, height, major, minor) { }

        public override void Initialise()
        {
            SetupStates();
            SetState("FractalScene");
        }

        private void SetupStates()
        {
            //stateManager.AddState("SceneGraphScene", new SceneGraphState(stateManager));
            //stateManager.AddState("ModelScene", new ModelState(stateManager));
            //stateManager.AddState("SkyboxScene", new Skyboxstate(stateManager));
            stateManager.AddState("FractalScene", new MandelbrotState(stateManager));
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
