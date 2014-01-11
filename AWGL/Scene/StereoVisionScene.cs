using AWGL.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace AWGL.Scene
{
    public class StereoVisionScene : DefaultScene
    {
        public StereoVisionScene()
        {
            this.VSync = VSyncMode.On;
        }

        #region Private Fields
        private TorusKnot obj;
        private float Angle;
        #endregion

        #region OnLoad
        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            obj = new TorusKnot(256, 32, 0.1, 3, 4, 1, true);
        }
        #endregion

        #region OnResize

        /// <summary>
        /// Respond to resize events here.
        /// </summary>
        /// <param name="e">Contains information on the new GameWindow size.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        #endregion

        #region OnRenderFrame

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Angle += (float)(e.Time * 20.0);

            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            SetupCamera(Eye.right);
            GL.ColorMask(true, false, false, true);
            Draw();

            GL.Clear(ClearBufferMask.DepthBufferBit); // 
            SetupCamera(Eye.left);
            GL.ColorMask(false, true, true, true);
            Draw();

            GL.ColorMask(true, true, true, true);

            SwapBuffers();
        }
        #endregion
    
        #region OnUnload
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            obj.Dispose();
        }
        #endregion

        #region Setup Camera(Eye eye)
        private void SetupCamera(Eye eye)
        {
            Camera camera;

            camera.Position = Vector3.UnitZ;
            camera.Up = Vector3.UnitY;
            camera.Direction = -Vector3.UnitZ;
            camera.NearPlane = 1.0;
            camera.FarPlane = 5.0;
            camera.FocalLength = 2.0;
            camera.EyeSeparation = camera.FocalLength / 30.0;
            camera.Aperture = 75.0;

            double left, right,
                   bottom, top;

            double widthdiv2 = camera.NearPlane * Math.Tan(MathHelper.DegreesToRadians((float)(camera.Aperture / 2.0))); // aperture in radians
            double precalc1 = ClientRectangle.Width / (double)ClientRectangle.Height * widthdiv2;
            double precalc2 = 0.5 * camera.EyeSeparation * camera.NearPlane / camera.FocalLength;

            Vector3 Right = Vector3.Cross(camera.Direction, camera.Up); // Each unit vectors
            Right.Normalize();

            Right.X *= (float)(camera.EyeSeparation / 2.0);
            Right.Y *= (float)(camera.EyeSeparation / 2.0);
            Right.Z *= (float)(camera.EyeSeparation / 2.0);

            // Projection Matrix
            top = widthdiv2;
            bottom = -widthdiv2;
            if (eye == Eye.right)
            {
                left = -precalc1 - precalc2;
                right = precalc1 - precalc2;
            }
            else
            {
                left = -precalc1 + precalc2;
                right = precalc1 + precalc2;
            }

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(left, right, bottom, top, camera.NearPlane, camera.FarPlane);

            // Modelview Matrix
            Matrix4 modelview;
            if (eye == Eye.right)
            {
                modelview = Matrix4.LookAt(
                    new Vector3(camera.Position.X + Right.X, camera.Position.Y + Right.Y, camera.Position.Z + Right.Z),
                    new Vector3(camera.Position.X + Right.X + camera.Direction.X, camera.Position.Y + Right.Y + camera.Direction.Y, camera.Position.Z + Right.Z + camera.Direction.Z),
                    camera.Up);
            }
            else
            {
                modelview = Matrix4.LookAt(
                    new Vector3(camera.Position.X - Right.X, camera.Position.Y - Right.Y, camera.Position.Z - Right.Z),
                    new Vector3(camera.Position.X - Right.X + camera.Direction.X, camera.Position.Y - Right.Y + camera.Direction.Y, camera.Position.Z - Right.Z + camera.Direction.Z),
                    camera.Up);
            }
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.MultMatrix(ref modelview);
        }
        #endregion

        #region Draw
        private void Draw()
        {
            GL.Translate(0f, 0f, -2f);
            GL.Rotate(Angle, Vector3.UnitY);
            obj.Draw();
        }
        #endregion

    }

    #region StereoVison Structs
    public struct Camera
    {
        public Vector3 Position, Direction, Up;
        public double NearPlane, FarPlane;
        public double EyeSeparation;
        public double Aperture; // FOV in degrees
        public double FocalLength;
    }

    public enum Eye
    {
        left,
        right,
    }
    #endregion
}
