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
        InputKeys lastKeyPressed;

        private void Awake()
        {
            stateMachine = new StateMachine<AbilityStates>();
            SetupStateMachine();
        }

        private void Update()
        {
            stateMachine.Update();
            lastKeyPressed = InputKeys.None;
        }

        protected void SetupStateMachine() { 
            
        }

        public void HandleInput(InputKeys key) { 
        
            lastKeyPressed = key;
        }

        public void HandleCancel()
        {
            lastKeyPressed = InputKeys.Cancel;
        }
    }
}