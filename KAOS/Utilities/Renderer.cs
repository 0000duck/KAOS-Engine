using Assimp;
using KAOS.Managers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace KAOS.Utilities
{
    /// <summary>
    /// Responsible for all rendering.
    /// </summary>
    public static class Renderer
    {
        #region Members
        internal static Matrix4 projectionMatrix, modelMatrix, viewMatrix;
        internal static Vector3 eyePosition;
        internal static int
            handle_projectionMatrix, handle_eyePosition, handle_viewMatrix,
            handle_centre, handle_scale, handle_iter, handle_modelMatrix, handle_viewMatrix2;
        #endregion

        public static void DrawImmediateModeVertex(Vector3d position, Color4 color, Vector2 uvs)
        {
            GL.Color4(color);
            GL.TexCoord2(uvs);
            GL.Vertex3(position);
        }

        public static void DrawSkyBox(TextureManager textureManager, BufferObject bufferObject)
        {
            GL.UseProgram(ShaderManager.Skybox.ID);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureManager.Get("skybox1").ID);

            GL.BindVertexArray(bufferObject.VaoID);
            GL.Disable(EnableCap.DepthTest);

            GL.UniformMatrix4(handle_viewMatrix, false, ref viewMatrix);

            GL.DrawElements(bufferObject.PrimitiveType, bufferObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Enable(EnableCap.DepthTest);
        }

        public static void DrawObject(TextureManager textureManager, BufferObject bufferObject)
        {
            GL.UseProgram(ShaderManager.Render.ID);
            GL.BindVertexArray(bufferObject.VaoID);


            GL.UniformMatrix4(handle_viewMatrix2, false, ref viewMatrix);
            GL.UniformMatrix4(handle_modelMatrix, false, ref modelMatrix);
            GL.UniformMatrix4(handle_projectionMatrix, false, ref projectionMatrix);
            GL.Uniform1(Renderer.handle_iter, 70);
            GL.Uniform2(Renderer.handle_centre, 0f, 0f);
            GL.Uniform1(Renderer.handle_scale, 2.2);

            //GL.BindTexture(TextureTarget.Texture1D, m_textureManager.Get("1d").ID);

            GL.DrawElements(bufferObject.PrimitiveType, bufferObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
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