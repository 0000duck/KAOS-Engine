using KAOS.Interfaces;
using KAOS.Managers;
using KAOS.Utilities;

namespace KAOS.States
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
