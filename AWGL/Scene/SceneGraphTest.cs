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
        AWNode m_sceneGraph;

        public void CreateSceneGraph()
        {

            AWPolygon poly1 = new AWPolygon();
            AWPolygon poly2 = new AWPolygon();
            AWPolygon poly3 = new AWPolygon();
            AWPolygon poly4 = new AWPolygon();
            AWGroupNode root = new AWGroupNode();
            
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

            root.AddChild(poly1);
            root.AddChild(poly2);
            root.AddChild(poly3);
            root.AddChild(poly4);

            m_sceneGraph = root;
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

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(0, 20, 20, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            m_sceneGraph.Render();

            SwapBuffers();
        }
    }
}
