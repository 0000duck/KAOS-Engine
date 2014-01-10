using AWGL.Abstract;
using AWGL.Shapes;
using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AWGL
{
    public class Display : GameWindow
    {
        private int vertexShaderID;
        private int fragmentShaderID;

        private int programID;

        private int attribute_vpos;
        private int attribute_vcolor;
        private int uniform_mview;

        private int vbo_position;
        private int vbo_color;
        private int vbo_mview;
        private int ibo_elements;

        private Vector3[] vertData;
        private Vector3[] colorData;
        protected List<Volume> objects = new List<Volume>();
        private int[] indiceData;

        private float time = 0.0f;

        public Display() : base(1024, 700, new GraphicsMode(32, 24, 0, 4))
        {

        }   

        private void initProgram()
        {
            programID = GL.CreateProgram();

            loadShader("VS.glsl", ShaderType.VertexShader, programID, out vertexShaderID);
            loadShader("FS.glsl", ShaderType.FragmentShader, programID, out fragmentShaderID);

            // Links shaders and output any errors
            GL.LinkProgram(programID);
            Console.WriteLine(GL.GetProgramInfoLog(programID));

            // Get the values we need, and also do a simple check to make sure the attributes were found.
            attribute_vpos = GL.GetAttribLocation(programID, "vPosition");
            attribute_vcolor = GL.GetAttribLocation(programID, "vColor");
            uniform_mview = GL.GetUniformLocation(programID, "modelview");

            if (attribute_vpos == -1 || attribute_vcolor == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            // This generates 4 separate buffers and stores their addresses in our variables. 
            // For multiple buffers like this, there's an option for generating multiple buffers 
            // and storing them in an array, but for simplicity's sake, we're keeping them in separate ints.
            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);
            GL.GenBuffers(1, out ibo_elements);

            Random rand = new Random();

            float xPos = -1.0f;
            for (int i = 0; i < 2; i++)
            {
                Sierpinski sier = new Sierpinski();

                sier.Position = new Vector3(xPos, 0.0f, -2.5f);
                sier.Rotation = new Vector3(0.55f, 0.25f, 0);
                sier.Scale = Vector3.One;
                objects.Add(sier);

                xPos = 1.0f;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            initProgram();

            Title = "AWGL - High level OpenTK wrapper";
            GL.ClearColor(Color.Black);
            GL.PointSize(3f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcolor);

            int indiceAt = 0;
            
            foreach (Volume v in objects)
            {
                GL.UniformMatrix4(uniform_mview, false, ref v.ModelViewProjectionMatrix);
                GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceAt*sizeof(uint));
                indiceAt += v.IndiceCount;
            }

            // Keep things clean:
            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcolor);
            GL.Flush();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            time += (float)e.Time;

            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();

            int vertCount = 0;

            foreach (Volume v in objects)
            {
                verts.AddRange(v.GetVerts().ToList());
                inds.AddRange(v.GetIndices().ToList());
                colors.AddRange(v.GetColorData().ToList());
                vertCount += v.VertCount;
            }

            vertData = verts.ToArray();
            indiceData = inds.ToArray();
            colorData = colors.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);  // 1. Bind vertex data to the buffer.
            GL.BufferData<Vector3>(                                 // 2. Send data.
                BufferTarget.ArrayBuffer, (IntPtr)(vertData.Length * Vector3.SizeInBytes), 
                vertData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(                                 // 3. Tell OpenGL to use the last buffer bound to.
                attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);     // 1. Bind color data to the buffer
            GL.BufferData<Vector3>(                                 // 2. Send Data
                BufferTarget.ArrayBuffer, (IntPtr)(colorData.Length * Vector3.SizeInBytes),
                colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(                                 // 3. Tell OpenGL to use the last buffer bound to.
                attribute_vcolor, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer, (IntPtr)(indiceData.Length * sizeof(int)),
                indiceData, BufferUsageHint.StaticDraw);
            
            // Rotate objects
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
            }

            // Send model view matrix
            foreach (Volume v in objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = 
                    Matrix4.CreatePerspectiveFieldOfView(1.0f, ClientSize.Width / (float)ClientSize.Height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
            }

            GL.UseProgram(programID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void loadShader(String filename, ShaderType type, int program, out int address)
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
    }
}
