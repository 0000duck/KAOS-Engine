using AWGL.Shapes;
using AWGL.Tutorial;
using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace AWGL.Scene
{
    /// <summary>
    /// Controls Main Window functions and sets up OpenGL
    /// </summary>
    public abstract class DefaultScene : GameWindow
    {
        public DefaultScene()
            : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {
            this.WindowState = WindowState.Fullscreen;
            Keyboard.KeyDown += Keyboard_KeyDown;
            this.VSync = VSyncMode.On;
        }   

        #region Private Fields

        private const float rotation_speed = 180.0f;
        private float angle;

        private struct Vbo { public int VboID, EboID, NumElements; }
        private Vbo[] vbo = new Vbo[2];

        private VertexPositionColor[] CubeVertices = new VertexPositionColor[]
        {
                new VertexPositionColor(-1.0f, -1.0f,  1.0f, Color.DarkRed),
                new VertexPositionColor( 1.0f, -1.0f,  1.0f, Color.DarkRed),
                new VertexPositionColor( 1.0f,  1.0f,  1.0f, Color.Gold),
                new VertexPositionColor(-1.0f,  1.0f,  1.0f, Color.Gold),
                new VertexPositionColor(-1.0f, -1.0f, -1.0f, Color.DarkRed),
                new VertexPositionColor( 1.0f, -1.0f, -1.0f, Color.DarkRed), 
                new VertexPositionColor( 1.0f,  1.0f, -1.0f, Color.Gold),
                new VertexPositionColor(-1.0f,  1.0f, -1.0f, Color.Gold) 
        };

        private readonly short[] CubeElements = new short[]
        {
            0, 1, 2, 2, 3, 0, // front face
            3, 2, 6, 6, 7, 3, // top face
            7, 6, 5, 5, 4, 7, // back face
            4, 0, 3, 3, 7, 4, // left face
            0, 1, 5, 5, 4, 0, // bottom face
            1, 5, 6, 6, 2, 1, // right face
        };

        #endregion  

        #region Particles

        protected static int MaxParticleCount = 2000;
        private int VisibleParticleCount;
        private VertexC4ubV3f[] VBO = new VertexC4ubV3f[MaxParticleCount];
        private ParticleAttribut[] ParticleAttributes = new ParticleAttribut[MaxParticleCount];

        // this struct is used for drawing
        struct VertexC4ubV3f
        {
            public byte R, G, B, A;
            public Vector3 Position;

            public static int SizeInBytes = 16;
        }

        // this struct is used for updates
        struct ParticleAttribut
        {
            public Vector3 Direction;
            public uint Age;

            //  more stuff could be here: Rotation, Radius, whatever
        }

        private uint VBOHandle;

        private float xPos = 0.1f;
        private float yPos = 0.1f;

        #endregion Particles

        #region Textures

        private Bitmap bitmap = new Bitmap("Data/Textures/logo.jpg");
        private int texture;

        #endregion

        private MengerSponge sponge;

        #region Keyboard_KeyDown

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <param name="sender">The KeyboardDevice which generated this event.</param>
        /// <param name="e">The key that was pressed.</param>
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }

        #endregion

        #region OnLoad

        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            TestOpenGLVersion();

            Title = "AWGL: High level OpenTK wrapper - " + GL.GetString(StringName.Renderer) + " (GL " + GL.GetString(StringName.Version) + ")";

            GL.ClearColor(.1f, 0f, .1f, 0f);
            GL.Enable(EnableCap.DepthTest);

            #region 3D Glasses Effect
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            sponge= new MengerSponge(1.0, Shapes.MengerSponge.eSubdivisions.Two, true);

            #endregion

            #region Texture
            //// Set our point parameters
            //GL.PointSize(5f);
            //GL.Enable(EnableCap.PointSprite);

            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            
            //GL.GenTextures(1, out texture);
            //GL.BindTexture(TextureTarget.Texture2D, texture);

            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            //BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            //    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //GL.TexImage2D(TextureTarget.Texture2D, 0 ,PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            //    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            //bitmap.UnlockBits(data);
            #endregion
            // set up vbo state - depreceted as of 3.0>> (?)
            #region particles
            //GL.EnableClientState(ArrayCap.ColorArray);
            //GL.EnableClientState(ArrayCap.VertexArray);

            // Generate the buffers
            //GL.GenBuffers(1, out VBOHandle);

            // Set it up
            //GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            //GL.ColorPointer(4, ColorPointerType.UnsignedByte, VertexC4ubV3f.SizeInBytes, (IntPtr)0);
            //GL.VertexPointer(3, VertexPointerType.Float, VertexC4ubV3f.SizeInBytes, (IntPtr)(4 * sizeof(byte)));

            //Random rndNum = new Random();
            //Vector3 tmp = new Vector3();

            //// generate some random stuff for the particle system
            //for (uint i = 0; i < MaxParticleCount; i++)
            //{
            //    if (xPos >= 4.0f)
            //    {
            //        xPos = -4.0f;
            //    }
            //    if (yPos >= 4.0f)
            //    {
            //        yPos = -4.0f;
            //    }
            //    VBO[i].R = (byte)rndNum.Next(0, 256);
            //    VBO[i].G = (byte)rndNum.Next(0, 256);
            //    VBO[i].B = (byte)rndNum.Next(0, 256);
            //    VBO[i].A = (byte)rndNum.Next(0, 256); // isn't actually used
            //    VBO[i].Position = new Vector3(xPos, yPos, -1.0f); // all particles are born at the origin

            //    // generate direction vector in the range [-0.25f...+0.25f] 
            //    // that's slow enough so you can see particles 'disappear' when they are respawned
            //    tmp.X = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
            //    tmp.Y = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
            //    tmp.Z = (float)((rndNum.NextDouble() - 0.5) * 0.5f);
            //    ParticleAttributes[i].Direction = tmp; // copy 
            //    ParticleAttributes[i].Age = 0;

            //    xPos = xPos + 0.0231f;
            //    yPos = yPos + 0.0253f;
            //}

            //VisibleParticleCount = 0;
            #endregion

        }

        #endregion

        #region OnResize

        /// <summary>
        /// Respond to resize events here.
        /// </summary>
        /// <param name="e">Contains information on the new GameWindow size.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
        }

        #endregion
        
        #region OnUpdateFrame

        /// <summary>
        /// Add your game logic here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            #region Particles
            //// will update particles here. When using a Physics SDK, it's update rate is much higher than
            //// the framerate and it would be a waste of cycles copying to the VBO more often than drawing it.
            //if (VisibleParticleCount < MaxParticleCount)
            //    VisibleParticleCount++;

            //Vector3 temp;

            //for (int i = MaxParticleCount - VisibleParticleCount; i < MaxParticleCount; i++)
            //{
            //    if (ParticleAttributes[i].Age >= MaxParticleCount)
            //    {
            //        // reset particle
            //        ParticleAttributes[i].Age = 0;
            //        VBO[i].Position = Vector3.Zero;
            //    }
            //    else
            //    {
            //        ParticleAttributes[i].Age += (uint)Math.Max(ParticleAttributes[i].Direction.LengthFast * 10, 1);
            //        Vector3.Multiply(ref ParticleAttributes[i].Direction, (float)e.Time, out temp);
            //        Vector3.Add(ref VBO[i].Position, ref temp, out VBO[i].Position);
            //    }
            //}
            #endregion

            #region 3D Lighting Effect
            
            #endregion
        }

        #endregion

        #region 3D Lighting Effect
        struct Camera
        {
            public Vector3 Position, Direction, Up;
            public double NearPlane, FarPlane;
            public double EyeSeparation;
            public double Aperture; // FOV in degrees
            public double FocalLength;
        }

        enum Eye
        {
            left,
            right,
        }

        void SetupCamera(Eye eye)
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

        float Angle;

        void Draw()
        {
            GL.Translate(0f, 0f, -2f);
            GL.Rotate(Angle, Vector3.UnitY);
            sponge.Draw();
        }
        #endregion

        #region OnRenderFrame

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //base.OnRenderFrame(e);
            Angle += (float)(e.Time * 20.0);

            this.Title = "AWGL: High level OpenTK wrapper - " + VisibleParticleCount + " Points. FPS: " + string.Format("{0:F}", 1.0 / e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region Particles
            //GL.PushMatrix();

            //GL.Translate(0f, 0f, -5f);

            //// Tell OpenGL to discard old VBO when done drawing it and reserve memory _now_ for a new buffer.
            //// without this, GL would wait until draw operations on old VBO are complete before writing to it
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * MaxParticleCount), IntPtr.Zero, BufferUsageHint.StreamDraw);
            //// Fill newly allocated buffer
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(VertexC4ubV3f.SizeInBytes * MaxParticleCount), VBO, BufferUsageHint.StreamDraw);
            //// Only draw particles that are alive
            //GL.DrawArrays(BeginMode.Points, MaxParticleCount - VisibleParticleCount, VisibleParticleCount);

            //GL.PopMatrix();
            #endregion

            #region Textures
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadIdentity();
            //GL.BindTexture(TextureTarget.Texture2D, texture);

            //GL.Begin(BeginMode.Quads);

            //GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-0.6f, -0.4f);
            //GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(0.6f, -0.4f);
            //GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(0.6f, 0.4f);
            //GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-0.6f, 0.4f);

            //GL.End();
            #endregion

            #region 3D Lighting Effect
            SetupCamera(Eye.right);
            GL.ColorMask(true, false, false, true);
            Draw();

            GL.Clear(ClearBufferMask.DepthBufferBit); // 
            SetupCamera(Eye.left);
            GL.ColorMask(false, true, true, true);
            Draw();

            GL.ColorMask(true, true, true, true);
            #endregion

            SwapBuffers();
        }

        #endregion

        #region OnUnload

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            //GL.DeleteBuffers(1, ref VBOHandle);
            sponge.Dispose();
        }

        #endregion

        #region GetOpenGLVersion

        /// <summary>
        /// Get OpenGL Version Information and check system meets requirements
        /// </summary>
        private void TestOpenGLVersion()
        {
            Version m_Version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            Version m_TargetLow = new Version(3, 1);
            Version m_TargetHigh = new Version(4, 1);
            if (m_Version < m_TargetLow)
            {
                throw new NotSupportedException(String.Format(
                    "OpenGL {0} is required (you only have {1}).", m_TargetLow, m_Version));
            }
            else if (m_Version > m_TargetHigh)
            {
                throw new NotSupportedException(String.Format(
                    "OpenGL {0} is required (you only have {1}).", m_TargetHigh, m_Version));
            }
        }

        #endregion

        #region LoadShader

        /// <summary>
        /// Helper Funtion for loading shaders
        /// </summary>
        /// <param name="filename">Filename of GLSL Shader</param>
        /// <param name="type">Type of GLSL Shader to load</param>
        /// <param name="program">Program ID to add Shader too</param>
        /// <param name="address">Shader Pointer</param>
        private void LoadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader("Shaders/" + filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        #endregion

        #region LoadVBO<TVertex> (TVertex[] vertices, short[] elements) where TVertex : struct

        Vbo LoadVBO<TVertex>(TVertex[] vertices, short[] elements) where TVertex : struct
        {
            Vbo handle = new Vbo();
            int size;

            // To create a VBO:
            // 1) Generate the buffer handles for the vertex and element buffers.
            // 2) Bind the vertex buffer handle and upload your vertex data. 
            //    Check that the buffer was uploaded correctly.
            // 3) Bind the element buffer handle and upload your element data. 
            //    Check that the buffer was uploaded correctly.

            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * BlittableValueType.StrideOf(vertices)), vertices,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * BlittableValueType.StrideOf(vertices) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * sizeof(short)), elements,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (elements.Length * sizeof(short) != size)
                throw new ApplicationException("Element data not uploaded correctly");

            handle.NumElements = elements.Length;
            return handle;
        }

        #endregion

        #region Draw(Vbo handle)

        void Draw(Vbo handle)
        {
            // To draw a VBO:
            // 1) Ensure that the VertexArray client state is enabled.
            // 2) Bind the vertex and element buffer handles.
            // 3) Set up the data pointers (vertex, normal, color) according to your vertex format.
            // 4) Call DrawElements. (Note: the last parameter is an offset into the element buffer
            //    and will usually be IntPtr.Zero).

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);

            GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(CubeVertices), new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, BlittableValueType.StrideOf(CubeVertices), new IntPtr(12));

            GL.DrawElements(BeginMode.Triangles, handle.NumElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }

        #endregion


    }
}
