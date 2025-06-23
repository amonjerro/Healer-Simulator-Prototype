using UnityEngine;

namespace Prototype
{
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