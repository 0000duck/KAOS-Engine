using KAOS.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace KAOS.Managers
{
    public class StateManager
    {
        Dictionary<string, IGameObject> stateStore = new Dictionary<string, IGameObject>();
        IGameObject currentState = null;

        public void Update(float elapsedTime)
        {
            if (currentState == null)
                return;
            currentState.Update(elapsedTime);
        }

        public void Render()
        {
            if (currentState == null)
                return;
            currentState.Render();
        }

        public void AddState(string stateName, IGameObject state)
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
