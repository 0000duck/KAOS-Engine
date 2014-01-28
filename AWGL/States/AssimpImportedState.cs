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
using System.Threading.Tasks;
using Assimp.Configs;
using Assimp;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

namespace AWGL.States
{
    public class AssimpImportedState :IGameObject
    {
        BufferObjectManager m_bufferObjectManager = new BufferObjectManager();
        BufferObject m_bufferObject;
        StateManager m_stateManager;
        ShaderManager m_shaderManager;

        

        public AssimpImportedState(StateManager stateManager, ShaderManager shaderManager)
        {
            m_stateManager = stateManager;
            m_shaderManager = shaderManager;
        }

        public void Render()
        {

        }

        public void Update(float elapsedTime)
        {

        }
    }
}
