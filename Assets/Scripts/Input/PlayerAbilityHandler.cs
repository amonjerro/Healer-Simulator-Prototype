using Prototype.StateMachine;
using UnityEngine;

namespace Prototype
{
    public enum InputKeys
    {
        One = 0,
        Two,
        Three,
        Cancel,
        None
    }

    public class PlayerAbilityHandler : MonoBehaviour {

        CharacterEventManager eventManager;
        StateMachine<AbilityStates> stateMachine;
        CastingState castingState;
        InputKeys lastKeyPressed;

        [SerializeField]
        AbilityStates currentState;

        private void Awake()
        {
            lastKeyPressed = InputKeys.None;
            stateMachine = new StateMachine<AbilityStates>();
            SetupStateMachine();
            eventManager = GetComponentInParent<CharacterEventManager>();
        }

        private void Update()
        {
            AbsPlayerAbilityState state = (AbsPlayerAbilityState)stateMachine.CurrentState;
            state.SetKey(lastKeyPressed);

            stateMachine.Update();
            currentState = stateMachine.GetCurrentState();


            lastKeyPressed = InputKeys.None;
        }

        protected void SetupStateMachine() { 
            // Create the states
            castingState = new CastingState();
            castingState.SetAbilityHandler(this);
            AwaitingState awaiting = new AwaitingState();
            awaiting.SetAbilityHandler(this);
            TargetingState targeting = new TargetingState();
            targeting.SetAbilityHandler(this);

            // Wire up their transitions
            awaiting.transitions[AbilityStates.ChooseTarget].TargetState = targeting;
            targeting.transitions[AbilityStates.ChooseAbility].TargetState = awaiting;
            targeting.transitions[AbilityStates.UsingAbility].TargetState = castingState;
            castingState.transitions[AbilityStates.ChooseAbility].TargetState = awaiting;

            //Start the state machine
            stateMachine.SetStartingState(awaiting);
        }

        public void HandleInput(InputKeys key) 
        {
            lastKeyPressed = key;
        }

        public void HandleCancel()
        {
            lastKeyPressed = InputKeys.Cancel;
        }

        public void PublishMessage(CharacterEvent ev)
        {
            eventManager.BroadcastCharacterEvent(ev);
        }
    }
}