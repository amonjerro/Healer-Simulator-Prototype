using UnityEngine;
using UnityEngine.InputSystem;

namespace Prototype
{
    public class PlayerController : MonoBehaviour
    {
        CharacterMovement movementComponent;
        private void Start()
        {
            movementComponent = GetComponent<CharacterMovement>();
        }

        private void OnMove(InputValue val)
        {
            Vector2 moveValue = val.Get<Vector2>();
            movementComponent.SetMoveVector(moveValue);
        }

        private void OnAbilityOne(InputValue val) {
        
        }

        private void OnAbilityTwo(InputValue val) { 
        
        }

        private void OnAbilityThree(InputValue val)
        {

        }
        
    }
}