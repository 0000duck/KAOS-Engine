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
        private readonly Dictionary<string, AbstractState> _states = new Dictionary<string, AbstractState>();
        private AbstractState _currentState = null;

        public void Update(float elapsedTime, float aspect)
        {
            if (_currentState == null)
                return;
            _currentState.Update(elapsedTime, aspect);
        }

        public void Render()
        {
            if (_currentState == null)
                return;
            _currentState.Render();
        }

        public void AddState(string stateName, AbstractState state)
        {
            Debug.Assert( Exists(stateName) == false );
            _states.Add(stateName, state);
        }

        public void ChangeState(string stateName)
        {
            Debug.Assert( Exists(stateName) );
            _currentState = _states[stateName];
        }

        public bool Exists(string stateName)
        {
            return _states.ContainsKey(stateName);
        }
    }
}
