using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWGL.Utilities
{
    class Renderer
    {
        public Renderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public void DrawImmediateModeVertex(Vector3d position, Color4 color, Vector2 uvs)
        {
            GL.Color4(color);
            GL.TexCoord2(uvs);
            GL.Vertex3(position);
        }

        public void DrawSprite(Sprite sprite)
        {
            GL.BindTexture(TextureTarget.Texture2D, sprite.Texture.ID);
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < Sprite.VertexAmount; i++)
            {
                DrawImmediateModeVertex(
                    sprite.VertexPositions[i],
                    sprite.VertexColours[i],
                    sprite.VertexUVs[i]);

            }
            GL.End();
        }

        public void DrawSkyBox()
        {

        }
    }
}
