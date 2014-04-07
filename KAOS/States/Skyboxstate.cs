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
    public class Skyboxstate : AbstractState
    {
        internal static string defaultSkyboxPath = "Data/Textures/skybox/";
        internal string[] skyboxFaces = new String[]
        {
            defaultSkyboxPath + "pos_x.bmp",
            defaultSkyboxPath + "neg_x.bmp",
            defaultSkyboxPath + "pos_y.bmp",
            defaultSkyboxPath + "neg_y.bmp",
            defaultSkyboxPath + "pos_z.bmp",
            defaultSkyboxPath + "neg_z.bmp",
        };

        internal float _rotation;

        public Skyboxstate(StateManager stateManager)
        {
            BufferManager = new VertexBufferManager();
            StateManager = stateManager;
            TextureManager = new TextureManager();

            TextureManager.LoadTexture1D("1d", "pal.bmp");

            LoadCubeMap();
            QueryShaders();

            _rotation = MathHelper.DegreesToRadians(90);

            LoadTestObject();
        }

        private void QueryShaders()
        {
            Renderer.HandleViewMatrix = GL.GetUniformLocation(ShaderManager.Skybox.Id, "view_matrix");

            Renderer.HandleProjectionMatrix = GL.GetUniformLocation(ShaderManager.Render.Id, "proj_matrix");
            Renderer.HandleModelMatrix = GL.GetUniformLocation(ShaderManager.Render.Id, "model_matrix");
            Renderer.HandleViewMatrix2 = GL.GetUniformLocation(ShaderManager.Render.Id, "view_matrix");

            Renderer.HandleCentre = GL.GetUniformLocation(ShaderManager.Render.Id, "center");
            Renderer.HandleScale = GL.GetUniformLocation(ShaderManager.Render.Id, "scale");
            Renderer.HandleGlobalTime = GL.GetUniformLocation(ShaderManager.Render.Id, "iter");
        }

        private void LoadCubeMap()
        {
            TextureManager.LoadSkyTexture("skybox1", skyboxFaces);
        }

        private void LoadTestObject()
        {
            BufferManager.GenerateVertexBuffer("SkyCube", new Cube(0, 0, 0), ShaderManager.Skybox.Id);
            BufferManager.GenerateVertexBuffer("MengerSponge", new MengerSponge(1.0, Shapes.MengerSponge.eSubdivisions.Two, true ), ShaderManager.Render.Id);
            BufferManager.GenerateVertexBuffer("Sphere", new SlicedSphere(2.0f, Vector3d.Zero, SlicedSphere.eSubdivisions.Eight, new SlicedSphere.eDir[] { SlicedSphere.eDir.All }, false), ShaderManager.Render.Id); 
        }

        public override void Update(float elapsedTime, float aspect)
        {
            Renderer.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), aspect, 0.1f, 100.0f);

            Renderer.ViewMatrix = Matrix4.Mult(Matrix4.Identity, Camera.GetViewMatrix());
            Renderer.ModelMatrix = Matrix4.CreateScale(2f);

            Renderer.EyePosition = Camera.Position;
        }

        public override void Render()
        {
            Renderer.DrawSkyBox(TextureManager, BufferManager.GetBuffer("SkyCube"));
            Renderer.DrawObject(TextureManager, BufferManager.GetBuffer("MengerSponge"));
        }
    }
}
