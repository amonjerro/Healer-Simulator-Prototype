using System.Collections.Generic;

namespace Prototype.StateMachine
{
    /// <summary>
    /// Abstract AI Character state
    /// </summary>
    public abstract class AICharacterState : AbsState<CharacterStates>
    {
        protected AIController controller;

        public AICharacterState(StateMachine<CharacterStates> stateMachine, AIController controller) {
            machine = stateMachine;
            transitions = new Dictionary<CharacterStates, Transition<CharacterStates>>();
            this.controller = controller;
        }

        
    }
}