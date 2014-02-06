using AWGL.Interfaces;
using AWGL.Managers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace AWGL.States
{
    public class SplashScreenState : IGameObject
    {
        double currentRotation = 0;
        double delay = 300;

        StateManager m_stateManager;
        public SplashScreenState(StateManager stateManager)
        {
            m_stateManager = stateManager;
        }

        public void Update(float elapsedTime)
        {
            delay--;
            if (delay <= 0){
                delay = 3;
                m_stateManager.SetState("Voxels");
            }
            currentRotation = 10 * elapsedTime;
        }

        public void Render()
        {
            GL.ClearColor(Color.MidnightBlue);

            GL.Rotate(currentRotation, 0, 1, 0);
            GL.Begin(PrimitiveType.Triangles);

            GL.Vertex3(new OpenTK.Vector3(-0.5f, 0f, 0f));
            GL.Vertex3(new OpenTK.Vector3(.5f, 0f, 0f));
            GL.Vertex3(new OpenTK.Vector3(0f, .5f, 0f));

            GL.End();
            GL.Finish();
        }
    }
}
