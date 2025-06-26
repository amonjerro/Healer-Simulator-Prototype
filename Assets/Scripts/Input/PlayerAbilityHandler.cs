using Prototype.StateMachine;
using UnityEngine;

namespace Prototype
{
    public enum InputKeys
    {
        None,
        One,
        Two,
        Three,
        Cancel
    }

    public class PlayerAbilityHandler : MonoBehaviour {


        StateMachine<AbilityStates> stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine<AbilityStates>();
            SetupStateMachine();
        }

        private void Update()
        {
            stateMachine.Update();
        }

        protected void SetupStateMachine() { 
            
        }

        public CharacterEvent HandleInput(InputKeys key) 
        {
            AbsPlayerAbilityState state = (AbsPlayerAbilityState)stateMachine.CurrentState;
            CharacterEvent e = state.GetAbilityEvent(key);
            return e;
        }

        public CharacterEvent HandleCancel()
        {
            AbsPlayerAbilityState state = (AbsPlayerAbilityState)stateMachine.CurrentState;
            CharacterEvent e = state.GetAbilityEvent(InputKeys.Cancel);
            return e;
        }
    }
}