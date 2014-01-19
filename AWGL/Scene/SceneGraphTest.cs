using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL.Scene
{
    class SceneGraphTest : DefaultScene
    {
        #region SceneGraph
        private AWNode m_sceneGraph;
        private AWGroupNode m_hook1, m_hook2;
        #endregion

        private const float m_rotationspeed = 180.0f;
        private float m_spinangle;

        public void CreateSceneGraph()
        {
            AWGroupNode root = new AWGroupNode();
            AWGraphLines graph = new AWGraphLines();

            AWGroupNode rt1 = new AWGroupNode();
            AWGroupNode rt2 = new AWGroupNode();

            AWGroupNode rt = new AWGroupNode();
            AWCube cube = new AWCube();

            root.AddChild(graph);

            root.AddChild(rt1);
            root.AddChild(rt2);

            rt1.AddChild(rt);
            rt2.AddChild(rt);

            rt1.SetTranslation(5, 0, 0);
            rt2.SetTranslation(-10, 2, 0);

            rt.AddChild(cube);

            m_sceneGraph = root;

            m_hook1 = rt1;
            m_hook2 = rt2;
        }

        public override void Setup(EventArgs e)
        {
            CreateSceneGraph();
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Resize(EventArgs e)
        {
            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            m_spinangle += m_rotationspeed * (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(m_eyeX, m_eyeY, m_eyeZ, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            m_hook1.SetRotation(m_spinangle, 0, 1, 0);
            m_hook2.SetRotation(-m_spinangle, 0, 0, 1);

            m_sceneGraph.Render();

            SwapBuffers();
        }
    }
}
