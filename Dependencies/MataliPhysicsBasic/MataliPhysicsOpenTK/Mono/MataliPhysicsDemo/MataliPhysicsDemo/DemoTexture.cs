/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoTexture
    {
        Demo demo;
        int textureHandle;
        string dataFileDirectory;
        string dataFileExt;
        Bitmap texture;
        Bitmap textureXZ;
        Bitmap texturePosY;
        Bitmap textureNegY;
        bool enableMipmapTexture;

        public int Handle { get { return textureHandle; } }
        public string FileDirectory { get { return dataFileDirectory; } }
        public string FileExt { get { return dataFileExt; } }
        public Bitmap Bitmap { get { return texture; } }
        public Bitmap BitmapXZ { get { return textureXZ; } }
        public Bitmap BitmapPosY { get { return texturePosY; } }
        public Bitmap BitmapNegY { get { return textureNegY; } }
        public bool EnableMipmap { get { return enableMipmapTexture; } }

        public DemoTexture(Demo demo, string fileExt, bool enableMipmap)
        {
            this.demo = demo;
            enableMipmapTexture = enableMipmap;
            textureHandle = -1;
            texture = textureXZ = textureNegY = texturePosY = null;
            dataFileDirectory = null;
            dataFileExt = fileExt;
        }

        public DemoTexture(Demo demo, string fileExt, Bitmap texture, bool enableMipmap)
        {
            this.demo = demo;
            enableMipmapTexture = enableMipmap;
            dataFileDirectory = null;
            dataFileExt = fileExt;

            Create(texture);
        }

        public DemoTexture(Demo demo, string fileDirectory, string fileExt, bool enableMipmap)
        {
            this.demo = demo;
            enableMipmapTexture = enableMipmap;
            textureHandle = -1;
            texture = textureXZ = textureNegY = texturePosY = null;
            dataFileDirectory = fileDirectory;
            dataFileExt = fileExt;
        }

        public DemoTexture(Demo demo, string fileDirectory, string fileExt, Bitmap texture, bool enableMipmap)
        {
            this.demo = demo;
            enableMipmapTexture = enableMipmap;
            dataFileDirectory = fileDirectory;
            dataFileExt = fileExt;

            Create(texture);
        }

        public DemoTexture(Demo demo, Bitmap textureXZ, Bitmap texturePosY, Bitmap textureNegY, bool enableMipmap)
        {
            this.demo = demo;
            enableMipmapTexture = enableMipmap;
            dataFileDirectory = null;
            dataFileExt = ".bmp";

            Create(textureXZ, texturePosY, textureNegY);
        }

        public void Create(Bitmap texture)
        {
            this.texture = texture;
            textureXZ = texturePosY = textureNegY = null;

            BitmapData data = null;

            GL.Enable(EnableCap.Texture2D);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureHandle);
            GL.BindTexture(TextureTarget.Texture2D, textureHandle);

            if (enableMipmapTexture && demo.EnableMipmapExtension)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmapSgis, 1);

                data = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.ReadOnly, texture.PixelFormat);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                texture.UnlockBits(data);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                data = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.ReadOnly, texture.PixelFormat);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                texture.UnlockBits(data);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
        }

        public void Create(Bitmap textureXZ, Bitmap texturePosY, Bitmap textureNegY)
        {
            this.textureXZ = textureXZ;
            this.texturePosY = texturePosY;
            this.textureNegY = textureNegY;
            texture = null;

            BitmapData data = null;

            GL.Enable(EnableCap.TextureCubeMap);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureHandle);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureHandle);

            if (enableMipmapTexture && demo.EnableCubeMapExtension && demo.EnableMipmapExtension)
            {
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.GenerateMipmapSgis, 1);

                data = textureXZ.LockBits(new Rectangle(0, 0, textureXZ.Width, textureXZ.Height), ImageLockMode.ReadOnly, textureXZ.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeX, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeZ, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveZ, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                textureXZ.UnlockBits(data);

                data = texturePosY.LockBits(new Rectangle(0, 0, texturePosY.Width, texturePosY.Height), ImageLockMode.ReadOnly, texturePosY.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveY, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                texturePosY.UnlockBits(data);

                data = textureNegY.LockBits(new Rectangle(0, 0, textureNegY.Width, textureNegY.Height), ImageLockMode.ReadOnly, textureNegY.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeY, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                textureNegY.UnlockBits(data);

                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }
            else
            {
                data = textureXZ.LockBits(new Rectangle(0, 0, textureXZ.Width, textureXZ.Height), ImageLockMode.ReadOnly, textureXZ.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeX, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeZ, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveZ, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                textureXZ.UnlockBits(data);

                data = texturePosY.LockBits(new Rectangle(0, 0, texturePosY.Width, texturePosY.Height), ImageLockMode.ReadOnly, texturePosY.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveY, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                texturePosY.UnlockBits(data);

                data = textureNegY.LockBits(new Rectangle(0, 0, textureNegY.Width, textureNegY.Height), ImageLockMode.ReadOnly, textureNegY.PixelFormat);
                GL.TexImage2D(TextureTarget.TextureCubeMapNegativeY, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                textureNegY.UnlockBits(data);

                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }

            GL.Disable(EnableCap.TextureCubeMap);
        }
    }
}
