using AWGL.Interfaces;
using AWGL.Managers;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AWGL.States
{
    public class DrawSpriteState : IGameObject
    {
        private ShaderManager m_shaderManager;
        private StateManager m_stateManager;

        double height, width, halfHeight, halfWidth, x, y, z;

        #region IGameObject States

        public void Update(float elapsedTime)
        {
            //throw new NotImplementedException();
        }

        public void Render()
        {
            GL.ClearColor(Color.Black);
            GL.Begin(PrimitiveType.Triangles);

            GL.Vertex3(new Vector3d(x-halfWidth, y+halfHeight, 0)); //top left
            GL.Vertex3(new Vector3d(x+halfWidth, y+halfHeight, 0)); //top right
            GL.Vertex3(new Vector3d(x-halfWidth, y-halfHeight, 0)); //bottom left

            GL.Vertex3(new Vector3d(x+halfWidth, y+halfHeight, 0)); //top right
            GL.Vertex3(new Vector3d(x+halfWidth, y+-halfHeight, 0)); //bottom right
            GL.Vertex3(new Vector3d(x-halfWidth, y-halfHeight, 0)); //bottom left

            GL.End();

        } 
        #endregion
        public DrawSpriteState(StateManager stateManager)
        {
            m_stateManager = stateManager;
            this.height = 200;
            this.width = 200;

            this.halfHeight = this.height / 2;
            this.halfWidth = this.width / 2;

            this.x = 0;
            this.y = 0;
            this.z = 2;
        }
    }
}
