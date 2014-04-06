using KAOS.Interfaces;
using KAOS.States;
using System.Collections.Generic;
using System.Diagnostics;

namespace KAOS.Managers
{
    /// <summary>
    /// Responsible for managing different game states such as splash screen, menus, etc...
    /// </summary>
    public class StateManager
    {
        private Dictionary<string, AbstractState> stateStore = new Dictionary<string, AbstractState>();
        AbstractState currentState = null;

        public void Update(float elapsedTime, float aspect)
        {
            if (currentState == null)
                return;
            currentState.Update(elapsedTime, aspect);
        }

        public void Render()
        {
            if (currentState == null)
                return;
            currentState.Render();
        }

        public void AddState(string stateName, AbstractState state)
        {
            Debug.Assert( Exists(stateName) == false );
            stateStore.Add(stateName, state);
        }

        public void ChangeState(string stateName)
        {
            Debug.Assert( Exists(stateName) );
            currentState = stateStore[stateName];
        }

        public bool Exists(string stateName)
        {
            return stateStore.ContainsKey(stateName);
        }
    }
}
