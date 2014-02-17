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
    public class Skyboxstate : IDisposable, IGameObject
    {
        private BufferObjectManager m_bufferManager;
        private StateManager m_stateManager;
        private TextureManager m_textureManager;

        static string defaultSkyboxPath = "Data/Textures/skybox/";
        string[] skyboxFaces = new String[]
        {
            defaultSkyboxPath + "pos_x.bmp",
            defaultSkyboxPath + "neg_x.bmp",
            defaultSkyboxPath + "pos_y.bmp",
            defaultSkyboxPath + "neg_y.bmp",
            defaultSkyboxPath + "pos_z.bmp",
            defaultSkyboxPath + "neg_z.bmp",
        };

        float aspect = 1024 / (float)600;
        float _rotation;

        public Skyboxstate(StateManager stateManager)
        {
            m_bufferManager = new BufferObjectManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();

            //m_textureManager.LoadTexture1D("1d", "pal.bmp");

            LoadCubeMap();
            QueryShaders();

            _rotation = MathHelper.DegreesToRadians(90);

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
        }

        private void LoadTestObject()
        {
            m_bufferManager.AddBufferObject("SkyCube", new Cube(0, 0, 0), ShaderManager.Skybox.ID);
            m_bufferManager.AddBufferObject("MengerSponge", new MengerSponge(1.0, Shapes.MengerSponge.eSubdivisions.Two, true ), ShaderManager.Render.ID); 
        }

        public void Update(float elapsedTime)
        {
            Renderer.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), aspect, 0.1f, 100.0f);

            Renderer.viewMatrix = Matrix4.Invert(Camera.GetViewMatrix());

            Renderer.modelViewMatrix = Matrix4.Mult(Renderer.viewMatrix, Matrix4.CreateTranslation(Camera.Position));
            Renderer.eyePosition = Camera.Position;
        }

        public void Render()
        {
            Renderer.DrawSkyBox(m_textureManager, m_bufferManager.GetBuffer("SkyCube"));
            Renderer.DrawObject(m_textureManager, m_bufferManager.GetBuffer("MengerSponge"));
        }

        public void Dispose()
        {
            m_textureManager.Dispose();
        }
    }
}
