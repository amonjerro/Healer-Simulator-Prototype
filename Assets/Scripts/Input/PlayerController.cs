using UnityEngine;
using UnityEngine.InputSystem;

namespace Prototype
{
    /// <summary>
    /// Class that receives all player input and distributes it logically through the rest of the Character architecture
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        CharacterEventManager eventManager;
        PlayerAbilityHandler abilityHandler;

        private void Start()
        {
            abilityHandler = GetComponent<PlayerAbilityHandler>();
            Character character = GetComponentInParent<Character>();
            eventManager = character.GetComponentInParent<CharacterEventManager>();
            ServiceLocator.Instance.GetService<AIDirectorService>().RegisterActor(ActorAttitude.Player, character);
        }

        /// <summary>
        /// Receives movement inputs
        /// </summary>
        /// <param name="val"></param>
        private void OnMove(InputValue val)
        {
            Vector2 moveValue = val.Get<Vector2>();
            CharacterEvent ev = new CharacterEvent<float>(CharacterEventTypes.Movement, moveValue.x);
            eventManager.BroadcastCharacterEvent(ev);
        }

        /// <summary>
        /// Broadcasts that the player wants to execute the first ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityOne(InputValue val) {
            abilityHandler.HandleInput(InputKeys.One);
        }


        /// <summary>
        /// Broadcasts that the player wants to execute the second ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityTwo(InputValue val) {
            abilityHandler.HandleInput(InputKeys.Two);
        }

        
        /// <summary>
        /// Broadcasts that the player wants to execute the third ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityThree(InputValue val){
            abilityHandler.HandleInput(InputKeys.Three);
        }

        private void OnCancel(InputValue val) {
            abilityHandler.HandleCancel();
        }

    }
}