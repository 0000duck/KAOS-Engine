using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGL;

using OpenTK.Graphics.OpenGL;
using AWGL.Managers;
using AWGL.States;

namespace Game
{
    class Game : AWEngineWindow
    {
        public StateManager stateManager;
        public TextureManager texManager;

        public Game(int width, int height, int major, int minor) : base(width, height, major, minor) { }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

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
            stateManager.AddState("VboState", new VboState(stateManager, shaderManager));

            stateManager.ChangeState("VboState");
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
