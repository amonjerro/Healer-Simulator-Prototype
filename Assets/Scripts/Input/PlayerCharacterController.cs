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

    public class PlayerCharacterController : AbsCharacterController
    {

        StateMachine<AbilityStates> stateMachine;
        InputKeys lastKeyPressed;

        bool[] abilityAvailability;

        [SerializeField]
        [Tooltip("Read-only. For debugging purposes.")]
        AbilityStates currentState;

        private void Awake()
        {
            lastKeyPressed = InputKeys.None;
            stateMachine = new StateMachine<AbilityStates>();
            SetupStateMachine();
            eventManager = GetComponentInParent<CharacterEventManager>();
            eventManager.onStatusChange += ProcessEvents;
        }

        private void Update()
        {
            AbsPlayerAbilityState state = (AbsPlayerAbilityState)stateMachine.CurrentState;
            state.SetKey(lastKeyPressed);

            stateMachine.Update();
            currentState = stateMachine.GetCurrentState();


            lastKeyPressed = InputKeys.None;
        }

        protected override void SetupStateMachine()
        {
            // Create the states
            CastingState castingState = new CastingState();
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

        public void PublishUIMessage(CharacterEvent ev)
        {
            eventManager.BroadcastUIEvent(ev);
        }

        public bool CheckAbilityAvailability(int index)
        {
            return index >= abilityAvailability.Length ? false : abilityAvailability[index];
        }

        protected override void ProcessEvents(CharacterEvent ev)
        {
            switch (ev.eventType)
            {
                case CharacterEventTypes.AbilityAvailabilityChange:
                    abilityAvailability = ev.EventValue as bool[];
                    return;
                case CharacterEventTypes.Death:
                    ServiceMessage serviceMessage = new ServiceMessage<bool>(true, ServiceMessageTypes.GameLose);
                    ServiceLocator.serviceAction?.Invoke(serviceMessage);
                    return;
                default:
                    return;
            }
        }
    }
}