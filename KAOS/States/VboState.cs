using KAOS.Interfaces;
using KAOS.Managers;
using KAOS.Shapes;
using KAOS.Utilities;
using OpenTK.Graphics.OpenGL;
using System;

namespace KAOS.States
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

            m_bufferObjectManager.AddBufferObject("test-cube", tempVBO, ShaderManager.Skybox.ID);
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
