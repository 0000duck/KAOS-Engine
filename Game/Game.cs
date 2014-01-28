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
        public StateManager stateManager;
        public TextureManager texManager;

        public Game(int width, int height, int major, int minor) : base(width, height, major, minor) { }

        public override void Initialise()
        {

            texManager = new TextureManager();

            texManager.LoadTexture("sprite1", "Data/Textures/logo.jpg");
            texManager.LoadTexture("sprite2", "Data/Textures/metal.jpg");

            stateManager = new StateManager();
            stateManager.AddState("Splash", new SplashScreenState(stateManager));
            stateManager.AddState("Default", new DefaultState(stateManager));
            stateManager.AddState("Drawing", new DrawSpriteState(stateManager, texManager));
            stateManager.AddState("TestTexture", new TestSpriteClassState(texManager));
            //stateManager.AddState("VboState", new VboState(stateManager, shaderManager));
            stateManager.AddState("Assimp-state", new AssimpImportedState(stateManager, shaderManager));

            stateManager.ChangeState("Assimp-state");
            //stateManager.ChangeState("VboState");
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
