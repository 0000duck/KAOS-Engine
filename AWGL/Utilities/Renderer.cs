using AWGL.Managers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWGL.Utilities
{
    public static class Renderer
    {
        internal static Matrix4 projectionMatrix, modelViewProjectionMatrix, modelViewMatrix;
        internal static int handle_projectionMatrix, handle_modelViewProjectionMatrix, handle_modelViewMatrix;

        public static void DrawImmediateModeVertex(Vector3d position, Color4 color, Vector2 uvs)
        {
            GL.Color4(color);
            GL.TexCoord2(uvs);
            GL.Vertex3(position);
        }

        public static void DrawSprite(Sprite sprite)
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

        public static void DrawSkyBox(TextureManager m_textureManager, BufferObject cubeObject)
        {
            GL.ClearBuffer(ClearBuffer.Color, 0, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.ClearBuffer(ClearBuffer.Depth, 0, new float[] { 1.0f });

            GL.UseProgram(ShaderManager.Get("Skybox").ID);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, m_textureManager.Get("skybox1").ID);

            GL.Disable(EnableCap.DepthTest);
            int temploc = GL.GetUniformLocation(ShaderManager.Get("Skybox").ID, "tex_cubemap");
            GL.Uniform1(temploc, 0);

            GL.UniformMatrix4(handle_modelViewMatrix, false, ref modelViewMatrix);
            //GL.Uniform3(eye_handle, ref eyeObjectSpace);
            GL.UniformMatrix4(handle_modelViewProjectionMatrix, false, ref modelViewProjectionMatrix);

            GL.BindVertexArray(cubeObject.VaoID);
            GL.DrawElements(cubeObject.PrimitiveType, cubeObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Enable(EnableCap.DepthTest);
        }

        public static void DrawWireframeVoxel(float length, float height, float width)
        {
            
        }

        public static void DrawChunk(Chunk chunk)
        {

        }

        internal static void ToggleWireframeOn()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }

        internal static void ToggleWireframeOff()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
    }
}
