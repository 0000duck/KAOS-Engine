using KAOS.Interfaces;
using KAOS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAOS.States
{
    public abstract class AbstractState : IDisposable, IGameObject
    {
        protected VertexBufferManager BufferManager;
        protected StateManager StateManager;
        protected TextureManager TextureManager;

        public abstract void Update(float elapsedTime, float aspect);
        public abstract void Render();

        public void Dispose()
        {
            TextureManager.Dispose();
        }
    }
}
