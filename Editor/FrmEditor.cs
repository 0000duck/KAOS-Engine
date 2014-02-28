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
using OpenTK;
using System.Diagnostics;

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

        Stopwatch sw = new Stopwatch();
        private void glControl1_Load(object sender, EventArgs e)
        {
            glControlLoaded = true;

            Text = Renderer.GetVersionInfo();
            Renderer.Load();
            Renderer.SetupViewport(ref glControl1);

            Application.Idle += Application_Idle; // press TAB twice after +=
            sw.Start();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Accumulate(milliseconds);
            Animate(milliseconds);
        }

        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }

        float rotation = 0;
        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotation += deltaRotation;
            glControl1.Invalidate();
        }

        double accumulator = 0;
        int idleCounter = 0;
        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                Text = Renderer.GetVersionInfo() + ": " + idleCounter.ToString() + "fps";
                accumulator -= 1000;
                idleCounter = 0; // don't forget to reset the counter!
            }
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glControlLoaded) // Play nice
                return;

            Renderer.DefaultRender(ref glControl1, x, rotation);
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
            
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area

            float aspect_ratio = w / (float)h;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        internal static void DefaultRender(ref OpenTK.GLControl glControl1, int x, float rotation)
        {
            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            if (glControl1.Focused) // Simple enough :)
                GL.Color3(Color.Yellow);
            else
                GL.Color3(Color.Blue);

            GL.Rotate(rotation, Vector3.UnitY); // OpenTK has this nice Vector3 class!

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawCube();

            glControl1.SwapBuffers();
        }
        
        private static void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }


        internal static void Load()
        {
            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
        }

        internal static string GetVersionInfo()
        {
            return GL.GetString(StringName.Vendor) + " " +
                GL.GetString(StringName.Renderer) + " " +
                GL.GetString(StringName.Version);
        }
    }
}
