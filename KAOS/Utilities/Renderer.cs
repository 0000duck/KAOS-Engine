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
        internal static Matrix4 ProjectionMatrix, ModelMatrix, ViewMatrix;
        internal static Vector3 EyePosition;
        internal static int HandleProjectionMatrix, HandleEyePosition, HandleViewMatrix, HandleModelMatrix, HandleViewMatrix2,
            HandleCentre, HandleScale, HandleGlobalTime, HandleResolution;
        #endregion

        public static void DrawImmediateModeVertex(Vector3d position, Color4 color, Vector2 uvs)
        {
            GL.Color4(color);
            GL.TexCoord2(uvs);
            GL.Vertex3(position);
        }

        public static void DrawSkyBox(TextureManager textureManager, VertexBuffer bufferObject)
        {
            GL.UseProgram(ShaderManager.Skybox.Id);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureManager.Get("skybox1").Id);

            GL.BindVertexArray(bufferObject.VaoId);
            GL.Disable(EnableCap.DepthTest);

            GL.UniformMatrix4(HandleViewMatrix, false, ref ViewMatrix);

            GL.DrawElements(bufferObject.PrimitiveType, bufferObject.IndicesData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Enable(EnableCap.DepthTest);
        }

        public static void DrawObject(TextureManager textureManager, VertexBuffer bufferObject)
        {
            GL.UseProgram(ShaderManager.Render.Id);
            GL.BindVertexArray(bufferObject.VaoId);


            GL.UniformMatrix4(HandleViewMatrix2, false, ref ViewMatrix);
            GL.UniformMatrix4(HandleModelMatrix, false, ref ModelMatrix);
            GL.UniformMatrix4(HandleProjectionMatrix, false, ref ProjectionMatrix);

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