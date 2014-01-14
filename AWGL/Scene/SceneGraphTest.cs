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

            AWPolygon poly1 = new AWPolygon();
            AWPolygon poly2 = new AWPolygon();
            AWPolygon poly3 = new AWPolygon();
            AWPolygon poly4 = new AWPolygon();
            AWGroupNode rt = new AWGroupNode();

            Vector3 a = new Vector3(.0f, .0f, 2.5f);
            Vector3 b = new Vector3(2.5f, .0f, -2.5f);
            Vector3 c = new Vector3(-2.5f, .0f, 2.5f);
            Vector3 d = new Vector3(.0f, 4.0f, .0f);

            poly1.AddNormal(new Vector3(.0f, -1.0f, .0f));
            poly1.AddVertex(0, c);
            poly1.AddVertex(1, b);
            poly1.AddVertex(2, c);

            poly2.AddNormal(new Vector3(.861411f, .269191f, .430706f));
            poly2.AddVertex(0, d);
            poly2.AddVertex(1, a);
            poly2.AddVertex(2, b);

            poly3.AddNormal(new Vector3(.0f, .529999f, -.847998f));
            poly3.AddVertex(0, d);
            poly3.AddVertex(1, b);
            poly3.AddVertex(2, c);

            poly4.AddNormal(new Vector3(-.861411f, .269191f, .430706f));
            poly4.AddVertex(0, d);
            poly4.AddVertex(1, c);
            poly4.AddVertex(2, a);

            AWGroupNode root = new AWGroupNode();
            AWGraphLines graph = new AWGraphLines();
            AWGroupNode rt1 = new AWGroupNode();
            AWGroupNode rt2 = new AWGroupNode();

            root.AddChild(graph);
            root.AddChild(rt1);
            root.AddChild(rt2);

            rt1.AddChild(rt);
            rt2.AddChild(rt);

            rt1.SetTranslation(5, 0, 0);
            rt2.SetTranslation(-5, 0, 0);

            rt.AddChild(poly1);
            rt.AddChild(poly2);
            rt.AddChild(poly3);
            rt.AddChild(poly4);

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
