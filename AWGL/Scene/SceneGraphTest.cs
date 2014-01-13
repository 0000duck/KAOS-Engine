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
            AWPolygon poly = new AWPolygon();
            
            poly.AddNormal(new Vector3(0.861411f, 0.269191f, 0.430706f));

            poly.AddVertex(0, new Vector3(.0f, 4.0f, .0f));
            poly.AddVertex(1, new Vector3(.0f, .0f, 2.5f));
            poly.AddVertex(2, new Vector3(2.5f, .0f, -2.5f));

            m_sceneGraph = poly;
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

            Matrix4 lookat = Matrix4.LookAt(0, 10, 10, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            m_sceneGraph.Render();

            SwapBuffers();
        }
    }
}
