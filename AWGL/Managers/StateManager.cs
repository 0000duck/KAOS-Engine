using AWGL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AWGL.Managers
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

        public void SetState(string stateName)
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
