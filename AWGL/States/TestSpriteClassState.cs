using AWGL.Interfaces;
using AWGL.Managers;
using AWGL.Utilities;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWGL.States
{
    public class TestSpriteClassState : IGameObject
    {
        Renderer m_renderer = new Renderer();
        TextureManager m_textureManager = new TextureManager();
        Sprite sprite1 = new Sprite();
        Sprite sprite2 = new Sprite();

        public TestSpriteClassState(TextureManager textureManager)
        {
            m_textureManager = textureManager;
            sprite1.Texture = m_textureManager.Get("sprite1");
            sprite1.SetHeight(256 * 0.5f);
            sprite1.SetPosition(new Vector3d(100, 100, 0));
            sprite1.SetColour(new Color4(256, 256, 256, 1));

            sprite2.Texture = m_textureManager.Get("sprite2");
            sprite2.SetHeight(256 * .5f);
            sprite2.SetPosition(new Vector3d(-100, -100, 0));
            sprite2.SetColour(new Color4(256, 256, 256, 1));
        }

        public void Update(float elapsedTime)
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            GL.ClearColor(1f, 1f, 1f, 1f);
            m_renderer.DrawSprite(sprite1);
            m_renderer.DrawSprite(sprite2);
            GL.Finish();
        }
    }
}
