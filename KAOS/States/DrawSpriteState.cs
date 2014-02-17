//using KAOS.Interfaces;
//using KAOS.Managers;
//using KAOS.Utilities;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;
//using System.Drawing;

//namespace KAOS.States
//{
//    public class DrawSpriteState : IGameObject
//    {
//        private StateManager m_stateManager;
//        private TextureManager m_textureManager;

//        double height, width, halfHeight, halfWidth, x, y, z;
//        float topUV, bottomUV, leftUV, rightUV;

//        #region IGameObject States

//        public void Update(float elapsedTime)
//        {
//            //throw new NotImplementedException();
//        }

//        public void Render()
//        {
//            Texture texture = m_textureManager.Get("sprite1");
//            GL.Enable(EnableCap.Texture2D);
//            GL.BindTexture(TextureTarget.Texture2D, texture.ID);


//            GL.ClearColor(Color.Black);
//            GL.Begin(PrimitiveType.Triangles);

//            GL.TexCoord2(new Vector2d(leftUV, topUV));
//            GL.Vertex3(new Vector3d(x - halfWidth, y + halfHeight, 0)); //top left
//            GL.TexCoord2(new Vector2d(rightUV, topUV));
//            GL.Vertex3(new Vector3d(x + halfWidth, y + halfHeight, 0)); //top right
//            GL.TexCoord2(new Vector2d(leftUV, bottomUV));
//            GL.Vertex3(new Vector3d(x - halfWidth, y - halfHeight, 0)); //bottom left

//            GL.TexCoord2(new Vector2d(rightUV, topUV));
//            GL.Vertex3(new Vector3d(x + halfWidth, y + halfHeight, 0)); //top right
//            GL.TexCoord2(new Vector2d(rightUV, bottomUV));
//            GL.Vertex3(new Vector3d(x + halfWidth, y + -halfHeight, 0)); //bottom right
//            GL.TexCoord2(new Vector2d(leftUV, bottomUV));
//            GL.Vertex3(new Vector3d(x - halfWidth, y - halfHeight, 0)); //bottom left

//            GL.End();

//        } 
//        #endregion

//        public DrawSpriteState(StateManager stateManager, TextureManager texturManager)
//        {
//            m_stateManager = stateManager;
//            m_textureManager = texturManager;
//            Initialise();
//        }

//        private void Initialise()
//        {
//            this.height = 200;
//            this.width = 200;

//            this.halfHeight = this.height / 2;
//            this.halfWidth = this.width / 2;

//            this.x = 0;
//            this.y = 0;
//            this.z = 2;

//            this.topUV = 0;
//            this.bottomUV = 1;
//            this.leftUV = 0;
//            this.rightUV = 1;
//        }
//    }
//}
