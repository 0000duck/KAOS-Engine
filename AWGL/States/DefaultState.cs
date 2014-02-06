using AWGL.Interfaces;
using AWGL.Managers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace AWGL.States
{
    public class DefaultState : IGameObject
    {
        double currentRotation = 0;

        StateManager m_stateManager;
        public DefaultState(StateManager stateManager)
        {
            m_stateManager = stateManager;
        }

        public void Update(float elapsedTime)
        {
            currentRotation = 10 * elapsedTime;
        }

        public void Render()
        {
            GL.ClearColor(Color.Black);
            GL.PointSize(5f);

            GL.Rotate(currentRotation, 0, 1, 0);
            GL.Begin(PrimitiveType.TriangleStrip);

            GL.Color4(new Color4(1f, 0f, 0f, .5f));
            GL.Vertex3(new Vector3(-50f, 0f, 0f));
            GL.Color3(new Vector3(0f, 1f, 0f));
            GL.Vertex3(new Vector3(50f, 0, 0));
            GL.Color3(new Vector3(0f, 0f, 1f));
            GL.Vertex3(new Vector3(0f, 50f, 0));

            GL.End();
            GL.Finish();
        }
    }
}
