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

        public AssimpImportedState(StateManager stateManager)
        {
            m_stateManager = stateManager;
        }

        public void Render()
        {

        }

        public void Update(float elapsedTime)
        {

        }
    }
}
