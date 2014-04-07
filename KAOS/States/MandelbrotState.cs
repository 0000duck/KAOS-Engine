using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using KAOS.Utilities;

namespace KAOS.States
{
    public class MandelbrotState : AbstractState
    {
        private float cx = 0.7f;
        private float cy = 0.0f;
        private float scale = 20.2f;
        private int iter = 70;
        private float zoom_factor = 0.025f;
        private int prog;
        private int texcoord_index;
        private int tex;

        #region Contructors

        public MandelbrotState(StateManager stateManager)
        {
            m_bufferManager = new VertexBufferManager();
            m_stateManager = stateManager;
            m_textureManager = new TextureManager();

            LoadPalette();
            LoadShader();
        }

        private void LoadPalette()
        {
            m_textureManager.LoadTexture1D("pal", "pal.bmp");
            //GL.Enable(EnableCap.Texture1D);
        }

        private void LoadShader()
        {
            ShaderManager.LoadCustomProgram("mbrot", "mbrot-vs", "mbrot-fs");

            prog = ShaderManager.Get("mbrot").ID;
            Renderer.handle_centre = GL.GetUniformLocation(prog, "center");
            Renderer.handle_scale = GL.GetUniformLocation(prog, "scale");
            Renderer.handle_iter = GL.GetUniformLocation(prog, "iter");

            GL.Uniform1(Renderer.handle_iter, iter);
            GL.Uniform2(Renderer.handle_centre, cx, cy);
            GL.Uniform1(Renderer.handle_scale, scale);
        }

        #endregion

        public override void Update(float elapsedTime, float aspect)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        public override void Render()
        {
            GL.UseProgram(prog);

            GL.Uniform2(Renderer.handle_centre, cx, cy);
            GL.Uniform1(Renderer.handle_scale, scale);

            GL.MatrixMode(MatrixMode.Texture);
            GL.LoadIdentity();

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, 1.0f);

            GL.End();
        }
    }
}
