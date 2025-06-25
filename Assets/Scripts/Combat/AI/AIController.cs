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
        ActorAttitude CharacterAttitude;

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
        }

        private void SetUpStateMachine()
        {
            WanderState wanderState = new WanderState(stateMachine, this);
            IdleState idleState = new IdleState(stateMachine, this);

            wanderState.transitions[CharacterStates.Idle].TargetState = idleState;
            idleState.transitions[CharacterStates.Wandering].TargetState = wanderState;
        }

        private void Update()
        {
            stateMachine.Update();
        }

        public float GetWanderDestination()
        {
            return directorRef.GetPlayerPosition() + Random.Range(-1.0f, 1.0f);
        }

        public void MoveInDirection(float direction) { 
            CharacterEvent ce = new CharacterEvent(CharacterEventTypes.Movement, direction);
            eventManager.BroadcastCharacterEvent(ce);
        }
    }
}