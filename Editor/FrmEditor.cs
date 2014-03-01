using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Editor
{
    public partial class Editor : Form
    {
        bool glControlLoaded = false;
        int x = 0;

        // time drift
        public static Stopwatch watch = new Stopwatch();
        double update_time, render_time;

        // timing information
        double timestamp;
        int update_count;
        int update_fps;
        int render_count;
        int render_fps;

        #region --- Constructor ---

        public Editor()
        {
            InitializeComponent();
        }

        #endregion

        #region OnLoad

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControlLoaded = true;

            Text = Renderer.GetVersionInfo();
            
            Renderer.Load();
            //Renderer.SetupViewport(ref glControl1);

            glControl1.MouseEnter += delegate { Renderer.mouse_in_glControl = true; };
            glControl1.MouseLeave += delegate { Renderer.mouse_in_glControl = false; };

            Application.Idle += Application_Idle; // press TAB twice after +=
            watch.Start();
        }

        #endregion

        #region OnClosing

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Idle -= Application_Idle;

            base.OnClosing(e);
        }

        #endregion

        #region Application_Idle event

        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Accumulate(milliseconds);
            Animate(milliseconds);
        }

        private double ComputeTimeSlice()
        {
            watch.Stop();
            double timeslice = watch.Elapsed.TotalMilliseconds;
            watch.Reset();
            watch.Start();
            return timeslice;
        }

        float rotation = 0;
        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotation += deltaRotation;

            Renderer.UpdateTextDisplay(ref watch);

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

        #endregion

        #region glControl1_Events

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glControlLoaded) // Play nice
                return;

            Renderer.DefaultRender(x, rotation);
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

        private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void glControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        #endregion

    }
}
