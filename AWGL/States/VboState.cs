using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWGL.Interfaces;
using AWGL.Managers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using AWGL.Shapes;
using AWGL.Utilities;
using System.Drawing;
using System.Diagnostics;

namespace AWGL.States
{
    public class VboState : IGameObject
    {
        BufferObjectManager m_bufferObjectManager = new BufferObjectManager();

        BufferObject m_bufferObject;
        StateManager m_stateManager;
        Cube cube = new Cube(0, 0, 0);

        public VboState(StateManager stateManager)
        {
            m_stateManager = stateManager;

            CreateVBOs();
        }

        private void CreateVBOs()
        {
            BufferObject tempVBO = new BufferObject();
            tempVBO.PositionData = cube.Vertices;
            tempVBO.NormalsData = cube.Normals;
            tempVBO.IndicesData = cube.Indices;
            tempVBO.PrimitiveType = PrimitiveType.Triangles;

            m_bufferObjectManager.AddBufferObject("test-cube", tempVBO, ShaderManager.DefaultShader.ID);
            m_bufferObject = m_bufferObjectManager.GetBuffer("test-cube");
        }

        public void Update(float elapsedTime)
        {

        }

        public void Render()
        {
            GL.BindVertexArray(m_bufferObject.VaoID);
            GL.DrawElements(m_bufferObject.PrimitiveType,
                            m_bufferObject.IndicesData.Length,
                            DrawElementsType.UnsignedInt,
                            IntPtr.Zero);
        }
    }
}
