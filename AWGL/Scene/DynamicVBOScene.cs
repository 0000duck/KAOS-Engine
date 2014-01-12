using AWGL.Shapes;
using AWGL.Tutorial;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGL.Scene
{
    public class DynamicVBOScene : DefaultScene
    {
        public DynamicVBOScene()
        {
            this.VSync = VSyncMode.Off;
        }

        #region Private Members
        protected static int m_MaxParticleCount = 2000;
        private int m_VisibleParticleCount;
        private VertexC4ubV3f[] m_VBO = new VertexC4ubV3f[m_MaxParticleCount];
        private ParticleAttribut[] m_ParticleAttributes = new ParticleAttribut[m_MaxParticleCount];

        private uint VBOHandle;

        private float xPos = 0.1f;
        private float yPos = 0.1f;
        #endregion Private Members

        #region OnLoad
        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);

            // Setup parameters for Points
            GL.PointSize(5f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            // set up vbo state - depreceted as of 3.0>> (?)
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

            // Generate the buffers
            GL.GenBuffers(1, out VBOHandle);

            // Set it up
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, VertexC4ubV3f.SizeInBytes, (IntPtr)0);
            GL.VertexPointer(3, VertexPointerType.Float, VertexC4ubV3f.SizeInBytes, (IntPtr)(4 * sizeof(byte)));

            Random rndNum = new Random();
            Vector3 tmp = Vector3.Zero;

            // generate some random stuff for the particle system
            for (uint i = 0; i < m_MaxParticleCount; i++)
            {
                m_VBO[i].R = (byte)rndNum.Next(0, 256);
                m_VBO[i].G = (byte)rndNum.Next(0, 256);
                m_VBO[i].B = (byte)rndNum.Next(0, 256);
                m_VBO[i].A = (byte)rndNum.Next(0, 256); // isn't actually used
                m_VBO[i].Position = Vector3.Zero; // all particles are born at the origin

                // generate direction vector in the range [-0.25f...+0.25f] 
                // that's slow enough so you can see particles 'disappear' when they are respawned
                tmp.X = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
                tmp.Y = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
                tmp.Z = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
                m_ParticleAttributes[i].Direction = tmp; // copy 
                m_ParticleAttributes[i].Age = 0;
            }

            m_VisibleParticleCount = 0;
        }
        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 p = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.1f, 50.0f);
            GL.LoadMatrix(ref p);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref mv);
        }

        #region OnUpdateFrame
        /// <summary>
        /// Add your game logic here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // will update particles here. When using a Physics SDK, it's update rate is much higher than
            // the framerate and it would be a waste of cycles copying to the VBO more often than drawing it.
            if (m_VisibleParticleCount < m_MaxParticleCount)
            {
                m_VisibleParticleCount++;
            }

            Vector3 temp;

            for (int i = m_MaxParticleCount - m_VisibleParticleCount; i < m_MaxParticleCount; i++)
            {
                if (m_ParticleAttributes[i].Age >= m_MaxParticleCount)
                {
                    // reset particle
                    m_ParticleAttributes[i].Age = 0;
                    m_VBO[i].Position = Vector3.Zero;
                }
                else
                {
                    m_ParticleAttributes[i].Age += (uint)Math.Max(m_ParticleAttributes[i].Direction.LengthFast * 10, 1);
                    Vector3.Multiply(ref m_ParticleAttributes[i].Direction, (float)e.Time, out temp);
                    Vector3.Add(ref m_VBO[i].Position, ref temp, out m_VBO[i].Position);
                }
            }
        }
        #endregion

        #region OnRenderFrame

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            this.Title = "AWGL: High level OpenTK wrapper - " + m_VisibleParticleCount + " Points. FPS: " + string.Format("{0:F}", 1.0 / e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();

            GL.Translate(0f, 0f, -5f);

            // Tell OpenGL to discard old VBO when done drawing it and reserve memory _now_ for a new buffer.
            // without this, GL would wait until draw operations on old VBO are complete before writing to it
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * m_MaxParticleCount), IntPtr.Zero, BufferUsageHint.StreamDraw);
            // Fill newly allocated buffer
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * m_MaxParticleCount), m_VBO, BufferUsageHint.StreamDraw);
            // Only draw particles that are alive
            GL.DrawArrays(BeginMode.Points, m_MaxParticleCount - m_VisibleParticleCount, m_VisibleParticleCount);

            GL.PopMatrix();

            SwapBuffers();
        }
        #endregion

        #region OnUnload

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            GL.DeleteBuffers(1, ref VBOHandle);
        }

        #endregion        
    
    
        protected override void Setup(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void Resize(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    

}
