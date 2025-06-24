using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// AI Character controller.
    /// This class acts as a counterpart to the player controller and sends the same signals
    /// to the character class via events
    /// </summary>
    public class AIController : MonoBehaviour
    {
        AIStrategy strategy;
        CharacterMovement characterMovement;
        

        private void Start()
        {
            characterMovement = GetComponentInParent<CharacterMovement>();
            
        }
    }
}