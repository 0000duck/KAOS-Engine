using KAOS.Utilities;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace KAOS.Managers
{
    /// <summary>
    /// Responsible for uploading textures to the GPU.
    /// </summary>
    public class TextureManager : IDisposable
    {
        private readonly Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();

        public Texture Get(string textureId)
        {
            return _textures[textureId];
        }

        private int _gpuHandle, _textureVao;
        private Bitmap _bitmap;
        private BitmapData _bitmapData;

        public void LoadTexture(string textureId, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            GL.GenTextures(1, out _gpuHandle);
            GL.BindTexture(TextureTarget.Texture2D, _gpuHandle);

            OpenImageFile(path);

            GL.TexImage2D(TextureTarget.Texture2D, 
                0, PixelInternalFormat.Rgba, _bitmapData.Width, _bitmapData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _bitmapData.Scan0);

            CloseImageFile();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            _textures.Add(textureId, new Texture(_gpuHandle, _textureVao, _bitmapData.Width, _bitmapData.Height));
        }

        public void LoadTexture1D(string textureId, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out _gpuHandle);
            GL.BindTexture(TextureTarget.Texture1D, _gpuHandle);

            OpenImageFile(path);

            GL.TexImage1D(TextureTarget.Texture1D,
                0, PixelInternalFormat.Rgba, _bitmapData.Width, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _bitmapData.Scan0);

            CloseImageFile();

            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);

            _textures.Add(textureId, new Texture(_gpuHandle, _textureVao, _bitmapData.Width, _bitmapData.Height));
        }

        public void LoadSkyTexture(string textureId, string[] path)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out _gpuHandle);
            GL.BindTexture(TextureTarget.TextureCubeMap, _gpuHandle);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            for (int face = 0; face < 6; face++)
            {
                OpenImageFile(path[face]);
                _bitmap.Save(face + ".bmp");
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + face, 
                    0, PixelInternalFormat.Rgba, _bitmapData.Width, _bitmapData.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _bitmapData.Scan0);

                GL.Finish();
                CloseImageFile();
            }

            //GL.Enable(EnableCap.TextureCubeMapSeamless);

            //GL.GenVertexArrays(1, out _textureVao);
            //GL.BindVertexArray(_textureVao);

            //GL.DepthFunc(DepthFunction.Lequal);

            _textures.Add(textureId, new Texture(_gpuHandle, _textureVao, _bitmapData.Width, _bitmapData.Height));
        }

        private void OpenImageFile(string path)
        {
            _bitmap = new Bitmap(path);

            _bitmapData = _bitmap.LockBits(new System.Drawing.Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        private void CloseImageFile()
        {
            _bitmap.UnlockBits(_bitmapData);
        }

        public void Dispose()
        {
            foreach (Texture t in _textures.Values)
            {
                GL.DeleteTextures(1, new int[] { t.Id });
            }
            _bitmap.Dispose();
        }
    }
}
