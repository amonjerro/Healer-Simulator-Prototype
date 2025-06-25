using System.Collections.Generic;
using Prototype;

namespace Prototype.StateMachine
{
    public abstract class AICharacterState : AbsState<CharacterStates>
    {
        protected AIController controller;

        public AICharacterState(StateMachine<CharacterStates> stateMachine, AIController controller) {
            machine = stateMachine;
            transitions = new Dictionary<CharacterStates, Transition<CharacterStates>>();
            this.controller = controller;
        }

        protected void Flush()
        {
            foreach (Transition<CharacterStates> transition in transitions.Values)
            {
                transition.ResetCondition();
            }
        }
    }
}