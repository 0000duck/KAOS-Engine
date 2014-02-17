using KAOS.Interfaces;
using KAOS.Managers;
using KAOS.Shapes;
using KAOS.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace KAOS.States
{
    public class Skyboxstate : IGameObject
    {
        private BufferObjectManager m_bufferManager;
        private StateManager m_stateManager;
        private TextureManager m_textureManager;

        Cube cube;
        BufferObject cubeObject;

        static string defaultSkyboxPath = "Data/Skyboxes/set 16/";
        string[] skyboxFaces = new String[]
        {
            defaultSkyboxPath + "pos_x.bmp",
            defaultSkyboxPath + "neg_x.bmp",
            defaultSkyboxPath + "pos_y.bmp",
            defaultSkyboxPath + "neg_y.bmp",
            defaultSkyboxPath + "pos_z.bmp",
            defaultSkyboxPath + "neg_z.bmp",
        };

        Matrix3 modelMatrix3, normalMatrix;
        Vector3 eyeObjectSpace;
        Vector3 trans;
        
        int eye_handle, skybox_vao;

        float aspect = 1024 / (float)600;
        float _rotation;

        public Skyboxstate(StateManager stateManager)
        {
            m_bufferManager = new BufferObjectManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();

            m_textureManager.LoadTexture1D("1d", "pal.bmp");

            LoadCubeMap();
            QueryShaders();

            _rotation = MathHelper.DegreesToRadians(90);
            trans = new Vector3(0f, 0f, -10f);

            LoadTestObject();
        }

        private void QueryShaders()
        {
            Renderer.handle_eyePosition = GL.GetUniformLocation(ShaderManager.Skybox.ID, "eye_position");
            Renderer.handle_viewMatrix = GL.GetUniformLocation(ShaderManager.Skybox.ID, "view_matrix");

            Renderer.handle_projectionMatrix = GL.GetUniformLocation(ShaderManager.Render.ID, "proj_matrix");
            Renderer.handle_modelViewMatrix = GL.GetUniformLocation(ShaderManager.Render.ID, "mv_matrix");

            Renderer.handle_centre = GL.GetUniformLocation(ShaderManager.Render.ID, "center");
            Renderer.handle_scale = GL.GetUniformLocation(ShaderManager.Render.ID, "scale");
            Renderer.handle_iter = GL.GetUniformLocation(ShaderManager.Render.ID, "iter");
        }

        private void LoadCubeMap()
        {
            m_textureManager.LoadSkyTexture("skybox1", skyboxFaces);
            GL.Enable(EnableCap.TextureCubeMapSeamless);

            GL.GenVertexArrays(1, out skybox_vao);
            GL.BindVertexArray(skybox_vao);

            GL.DepthFunc(DepthFunction.Lequal);
        }

        private void LoadTestObject()
        {
            cube = new Cube(0, 0, 0);
            m_bufferManager.AddBufferObject("SkyCube", cube, ShaderManager.Skybox.ID);
            //m_bufferManager.AddBufferObject("Cube", cube, ShaderManager.Render.ID);
            m_bufferManager.AddBufferObject("Torus", new TorusKnot( 256, 32, 0.1, 3, 4, 1, true ), ShaderManager.Render.ID);

            //m_bufferManager.AddBufferObject("SkyCube", cubeObject, ShaderManager.Skybox.ID);
            //m_bufferManager.AddBufferObject("Cube", cubeObject, ShaderManager.Render.ID);
        }

        public void Update(float elapsedTime)
        {
            MoveCamera();

            Renderer.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), aspect, 0.1f, 100.0f);

            Renderer.viewMatrix = Matrix4.Invert(Camera.GetViewMatrix());

            Renderer.modelViewMatrix = Matrix4.Mult(Renderer.viewMatrix, Matrix4.CreateTranslation(Camera.Position));
            Renderer.eyePosition = Camera.Position;
        }

        public void Render()
        {
            cubeObject = m_bufferManager.GetBuffer("SkyCube");
            Renderer.DrawSkyBox(m_textureManager, cubeObject);
            
            cubeObject = m_bufferManager.GetBuffer("Torus");
            GL.BindVertexArray(cubeObject.VaoID);
            GL.UseProgram(ShaderManager.Render.ID);

            GL.UniformMatrix4(Renderer.handle_modelViewMatrix, false, ref Renderer.modelViewMatrix);
            GL.UniformMatrix4(Renderer.handle_projectionMatrix, false, ref Renderer.projectionMatrix);
            GL.Uniform1(Renderer.handle_iter, 70);
            GL.Uniform2(Renderer.handle_centre, 0f, 0f);
            GL.Uniform1(Renderer.handle_scale, 2.2);


            //GL.BindTexture(TextureTarget.Texture1D, m_textureManager.Get("1d").ID);

            GL.DrawElements(PrimitiveType.Triangles, cubeObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        #region Input Control
        private void MoveCamera()
        {
            foreach (Key key in InputManager.keyList)
            {

                switch (key)
                {
                    case Key.W:
                        Camera.Move(0f, 0.1f, 0f);
                        break;

                    case Key.A:
                        Camera.Move(-0.1f, 0f, 0f);
                        break;

                    case Key.S:
                        Camera.Move(0f, -0.1f, 0f);
                        break;

                    case Key.D:
                        Camera.Move(0.1f, 0f, 0f);
                        break;

                    case Key.Q:
                        Camera.Move(0f, 0f, 0.1f);
                        break;

                    case Key.E:
                        Camera.Move(0f, 0f, -0.1f);
                        break;

                    case Key.F1:
                        Renderer.ToggleWireframeOn();
                        break;

                    case Key.F2:
                        Renderer.ToggleWireframeOff();
                        break;

                    default:
                        break;
                }


            }
        #endregion
        }
    }
}
