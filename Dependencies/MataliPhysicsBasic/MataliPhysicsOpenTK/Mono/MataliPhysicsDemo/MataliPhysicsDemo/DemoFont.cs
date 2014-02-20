/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Komires.MataliPhysics;
using Komires.MataliRender;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoFont
    {
        Demo demo;
        int textureHandle;
        float[] fontTranslations = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 6, 11, 8, 13, 10, 4, 6, 6, 8, 11, 5, 6, 5, 6, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 6, 6, 11, 11, 11, 7, 13, 9, 9, 9, 10, 8, 8, 10, 10, 6, 6, 9, 8, 11, 10, 10, 8, 10, 9, 8, 9, 10, 9, 13, 9, 9, 8, 6, 6, 6, 11, 8, 8, 8, 8, 7, 8, 8, 5, 8, 8, 4, 5, 8, 4, 12, 8, 8, 8, 8, 6, 7, 5, 8, 8, 11, 7, 8, 7, 7, 6, 7, 11, 13, 13, 13, 13, 13, 4, 1, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 4, 5, 8, 8, 8, 8, 6, 8, 8, 13, 7, 9, 11, 6, 13, 8, 7, 11, 7, 7, 8, 8, 8, 6, 8, 7, 7, 9, 13, 13, 13, 7, 9, 9, 9, 9, 9, 9, 13, 9, 8, 8, 8, 8, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 11, 10, 10, 10, 10, 10, 9, 8, 8, 8, 8, 8, 8, 8, 8, 13, 7, 8, 8, 8, 8, 4, 4, 4, 4, 8, 8, 8, 8, 8, 8, 8, 11, 8, 8, 8, 8, 8, 8, 8, 8 };
        int fontVertexBuffer;
        VertexPositionColorTexture[] fontVertices;

        Matrix4 world;
        Matrix4 view;
        Matrix4 projection;

        public RenderPCT Render;

        public DemoFont(Demo demo, string fontTextureName, int maxTextLength)
        {
            this.demo = demo;
            textureHandle = demo.Textures[fontTextureName].Handle;

            fontVertices = new VertexPositionColorTexture[maxTextLength << 2];

            GL.GenBuffers(1, out fontVertexBuffer);

            Resize();

            Render = new RenderPCT();
        }

        public void Resize()
        {
            world = view = Matrix4.Identity;
            projection = Matrix4.CreateOrthographicOffCenter(0, demo.Width, 0, demo.Height, -1.0f, 1.0f);
        }

        public void Begin()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, fontVertexBuffer);
            GL.VertexPointer(3, VertexPointerType.Float, 24, IntPtr.Zero);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, 24, (IntPtr)12);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 24, (IntPtr)16);
            Render.SetWorld(ref world);
            Render.SetView(ref view);
            Render.SetProjection(ref projection);
            Render.EnableTexture = true;
            Render.Texture = textureHandle;
        }

        public void End()
        {
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, IntPtr.Zero);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }

        public void Draw(float x, float y, float scaleX, float scaleY, string text, Color color)
        {
            int fontValue, textLength, textBufferLength, fontColor;
            float u, v, translation, fontSizeX, fontSizeY, fontWidth, fontHeight, fontMargin;
            Matrix4 fontTranslation, fontWorld;

            Render.GetWorld(ref world);

            Matrix4.CreateTranslation(x, demo.Height - y - 16 * scaleY, 0.0f, out fontTranslation);
            Matrix4.Mult(ref world, ref fontTranslation, out fontWorld);

            Render.SetWorld(ref fontWorld);

            translation = 0.0f;
            fontSizeX = 16 * scaleX;
            fontSizeY = 16 * scaleY;
            fontWidth = 1.0f / 16.0f;
            fontHeight = 1.0f / 16.0f;
            fontMargin = 1.0f / 256.0f;
            fontColor = color.ToArgb();

            for (int i = 0, j = 0; i < text.Length; i++, j += 4)
            {
                fontValue = (int)text[i];
                u = (float)(fontValue % 16) * fontWidth + fontMargin;
                v = (float)(fontValue >> 4) * fontHeight + fontMargin;

                fontVertices[j].Position.X = translation;
                fontVertices[j].Position.Y = 0.0f;
                fontVertices[j].Position.Z = 0.0f;
                fontVertices[j].TextureCoordinate.X = u;
                fontVertices[j].TextureCoordinate.Y = v + fontHeight;
                fontVertices[j].Color = fontColor;

                fontVertices[j + 1].Position.X = fontSizeX + translation;
                fontVertices[j + 1].Position.Y = 0.0f;
                fontVertices[j + 1].Position.Z = 0.0f;
                fontVertices[j + 1].TextureCoordinate.X = u + fontWidth;
                fontVertices[j + 1].TextureCoordinate.Y = v + fontHeight;
                fontVertices[j + 1].Color = fontColor;

                fontVertices[j + 2].Position.X = translation;
                fontVertices[j + 2].Position.Y = fontSizeY;
                fontVertices[j + 2].Position.Z = 0.0f;
                fontVertices[j + 2].TextureCoordinate.X = u;
                fontVertices[j + 2].TextureCoordinate.Y = v;
                fontVertices[j + 2].Color = fontColor;

                fontVertices[j + 3].Position.X = fontSizeX + translation;
                fontVertices[j + 3].Position.Y = fontSizeY;
                fontVertices[j + 3].Position.Z = 0.0f;
                fontVertices[j + 3].TextureCoordinate.X = u + fontWidth;
                fontVertices[j + 3].TextureCoordinate.Y = v;
                fontVertices[j + 3].Color = fontColor;

                translation += fontTranslations[fontValue] * scaleX;
            }

            textLength = text.Length << 2;
            textBufferLength = 24 * textLength;

            Render.Apply();

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)textBufferLength, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)textBufferLength, fontVertices, BufferUsageHint.DynamicDraw);

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, textLength);

            Render.SetWorld(ref world);
        }
    }
}
