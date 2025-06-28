using UnityEngine;
using Prototype.StateMachine;

namespace Prototype
{
    /// <summary>
    /// AI Character controller.
    /// This class acts as a counterpart to the player controller and sends the same signals
    /// to the character class via events
    /// </summary>
    public class AIController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Is this AI friendly or hostile?")]
        ActorAttitude CharacterAttitude;

        [SerializeField]
        [Tooltip("Debugging for the state machine. Changing this in runtime does nothing.")]
        CharacterStates activeState;

        StateMachine<CharacterStates> stateMachine;
        CharacterEventManager eventManager;
        AIDirectorService directorRef;

        private void Start()
        {
            eventManager = GetComponentInParent<CharacterEventManager>();
            Character character = GetComponentInParent<Character>();
            directorRef = ServiceLocator.Instance.GetService<AIDirectorService>();
            directorRef.RegisterActor(CharacterAttitude, character);
            stateMachine = new StateMachine<CharacterStates>();
            SetupStateMachine();
        }

        /// <summary>
        /// Sets up the state machine for an AI controller. Part of initialization.
        /// Internals will be expanded as the AI becomes more complex.
        /// </summary>
        protected void SetupStateMachine()
        {
            WanderState wanderState = new WanderState(stateMachine, this);
            IdleState idleState = new IdleState(stateMachine, this);

            wanderState.transitions[CharacterStates.Idle].TargetState = idleState;
            idleState.transitions[CharacterStates.Wandering].TargetState = wanderState;

            stateMachine.SetStartingState(idleState);
        }

        private void Update()
        {
            stateMachine.Update();
            activeState = stateMachine.GetCurrentState();
        }

        /// <summary>
        /// Determines a wander destination point. This might be moved to a more complex AI Strategy pattern down the line
        /// </summary>
        /// <returns>The x position to wander towards</returns>
        public float GetWanderDestination()
        {
            return directorRef.GetPlayerPosition() + Random.Range(-1.0f, 1.0f);
        }

        /// <summary>
        /// Tells the AI in which direction to move.
        /// </summary>
        /// <param name="direction">Negative values represent left.</param>
        public void MoveInDirection(float direction) {
            CharacterEvent ce = new CharacterEvent<float>(CharacterEventTypes.Movement, direction);
            eventManager.BroadcastCharacterEvent(ce);
        }
    }
}