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

        private AWGroupNode worldRoot, landRoot;
        private AWGraphLines graph;

        private AWCube cube;

        private AWGroupNode m_hook1;
        #endregion

        private const float m_rotationspeed = 180.0f;
        private float m_spinangle;

        public void CreateSceneGraph()
        {
            InitialiseNodes();

            worldRoot.AddChild(graph);
            worldRoot.AddChild(landRoot);

            landRoot.SetTranslation(0, 0, -10);
            landRoot.AddChild(cube);

            m_sceneGraph = worldRoot;

            m_hook1 = landRoot;
        }

        private void InitialiseNodes()
        {
            worldRoot = new AWGroupNode();
            landRoot = new AWGroupNode();

            graph = new AWGraphLines(); ;
            cube = new AWCube();
        }

        public override void Setup(EventArgs e)
        {
            CreateSceneGraph();
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Resize(EventArgs e)
        {
            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = camera.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            m_spinangle += m_rotationspeed * (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = camera.GetViewMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            m_hook1.SetRotation(m_spinangle, 0, 1, 0);

            m_sceneGraph.Render();

            SwapBuffers();
        }
    }
}
