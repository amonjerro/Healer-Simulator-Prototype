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
        CharacterMovement characterMovement;
        

        private void Start()
        {
            characterMovement = GetComponentInParent<CharacterMovement>();
            Character character = GetComponentInParent<Character>();
            ServiceLocator.Instance.GetService<AIDirectorService>().RegisterActor(CharacterAttitude, character);
            stateMachine = new StateMachine<CharacterStates>();
        }

        private void SetUpStateMachine()
        {

        }

    }
}