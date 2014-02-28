using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace Editor
{
    public partial class Editor : Form
    {
        bool glControlLoaded = false;
        int x = 0;

        public Editor()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControlLoaded = true;
            Renderer.Load();
            Renderer.SetupViewport(ref glControl1);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glControlLoaded) // Play nice
                return;

            Renderer.DefaultRender(ref glControl1, x);
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                x++;
                glControl1.Invalidate();
            }
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            Renderer.Resize(ref glControl1);
        }

    }

    static class Renderer
    {

        internal static void Resize(ref OpenTK.GLControl glControl1)
        {
            SetupViewport(ref glControl1);
            glControl1.Invalidate();
        }

        internal static void SetupViewport(ref OpenTK.GLControl glControl1)
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
        }

        internal static void DefaultRender(ref OpenTK.GLControl glControl1, int x)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Translate(x, 0, 0); // position triangle according to our x variable

            if (glControl1.Focused) // Simple enough :)
                GL.Color3(Color.Yellow);
            else
                GL.Color3(Color.Blue);

            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(10, 20);
            GL.Vertex2(100, 20);
            GL.Vertex2(100, 50);
            GL.End();

            glControl1.SwapBuffers();
        }

        internal static void Load()
        {
            GL.ClearColor(Color.SkyBlue);
        }
    }
}
