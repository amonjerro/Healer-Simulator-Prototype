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

        private void Start()
        {
            eventManager = GetComponent<CharacterEventManager>();
        }

        /// <summary>
        /// Receives movement inputs
        /// </summary>
        /// <param name="val"></param>
        private void OnMove(InputValue val)
        {
            Vector2 moveValue = val.Get<Vector2>();
            CharacterEvent ev = new CharacterEvent(CharacterEventTypes.Movement, moveValue.x);
            CharacterEventManager.BroadcastCharacterEvent(ev);
        }

        /// <summary>
        /// Broadcasts that the player wants to execute the first ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityOne(InputValue val) {
        
        }


        /// <summary>
        /// Broadcasts that the player wants to execute the second ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityTwo(InputValue val) { 
        
        }


        /// <summary>
        /// Broadcasts that the player wants to execute the third ability in their loadout
        /// </summary>
        /// <param name="val"></param>
        private void OnAbilityThree(InputValue val)
        {

        }
        
    }
}