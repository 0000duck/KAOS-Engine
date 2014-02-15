using KAOS.Utilities;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace KAOS.Managers
{
    public class TextureManager : IDisposable
    {
        Dictionary<string, Texture> m_textureDatabase = new Dictionary<string, Texture>();

        public Texture Get(string textureId)
        {
            return m_textureDatabase[textureId];
        }

        private int textureGpuHandle;
        private Bitmap bitmap;
        private BitmapData bitmapData;

        public void LoadTexture(string textureId, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            GL.GenTextures(1, out textureGpuHandle);
            GL.BindTexture(TextureTarget.Texture2D, textureGpuHandle);

            OpenImageFile(path);

            GL.TexImage2D(TextureTarget.Texture2D, 
                0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

            CloseImageFile();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            m_textureDatabase.Add(textureId, new Texture(textureGpuHandle, bitmapData.Width, bitmapData.Height));
        }

        public void LoadSkyTexture(string textureId, string[] path)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out textureGpuHandle);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureGpuHandle);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            for (int face = 0; face < 6; face++)
            {
                OpenImageFile(path[face]);
                bitmap.Save(face + ".bmp");
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + face, 
                    0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

                GL.Finish();
                CloseImageFile();
            }

            m_textureDatabase.Add(textureId, new Texture(textureGpuHandle, bitmapData.Width, bitmapData.Height));
        }

        private void OpenImageFile(string path)
        {
            bitmap = new Bitmap(path);

            bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        private void CloseImageFile()
        {
            bitmap.UnlockBits(bitmapData);

            CleanUp();
        }

        private void CleanUp()
        {
            bitmap.Dispose();
        }


        public void Dispose()
        {
            foreach (Texture t in m_textureDatabase.Values)
            {
                GL.DeleteTextures(1, new int[] { t.ID });
            }
            bitmap.Dispose();
        }
    }
}
