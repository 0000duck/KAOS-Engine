using AWGL.Utilities;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace AWGL.Managers
{
    public class TextureManager : IDisposable
    {
        Dictionary<string, Texture> m_textureDatabase = new Dictionary<string, Texture>();

        public Texture Get(string textureId)
        {
            return m_textureDatabase[textureId];
        }

        private Bitmap bitmap;
        private BitmapData bitmapData;

        private int textureGpuHandle;

        public void LoadTexture(string textureId, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            GL.GenTextures(1, out textureGpuHandle);
            GL.BindTexture(TextureTarget.Texture2D, textureGpuHandle);
            
            try
            {
                bitmap = new Bitmap(path);
                bitmap.Save("test.bmp", ImageFormat.Bmp);
                bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            catch (Exception e)
            {
                Logger.WriteLine("Error loading texture.");
            }


            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

            bitmap.UnlockBits(bitmapData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            
            m_textureDatabase.Add(textureId, new Texture(textureGpuHandle, bitmapData.Width, bitmapData.Height));
        }

        #region MyRegion

        #endregion


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
