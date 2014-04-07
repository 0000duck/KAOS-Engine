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
using OpenTK;

namespace KAOS.States
{
    public class MandelbrotState : AbstractState
    {
        private Vector3 iResolution = new Vector3(1280, 720, 0);
        private float iGlobalTime = 0.0f;
        private int prog;

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
        }

        private void LoadShader()
        {
            ShaderManager.LoadCustomProgram("mbrot", "mbrot-vs", "mbrot-fs");

            prog = ShaderManager.Get("mbrot").ID;
            Renderer.handle_iResolution = GL.GetUniformLocation(prog, "iResolution");

            GL.Uniform2(Renderer.handle_iResolution, iResolution.X, iResolution.Y);
            
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
            //GL.UseProgram(prog);

            GL.MatrixMode(MatrixMode.Modelview);
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
