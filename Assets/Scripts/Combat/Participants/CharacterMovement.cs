using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Class that handles all movement for a particular character.
    /// Governs PCs and NPCs.
    /// </summary>
    public class CharacterMovement : MonoBehaviour
    {
        float directionX;
        float moveVelocity;
        Transform parentObject;

        private void Start()
        {
            parentObject = transform.parent;
            parentObject.GetComponent<CharacterEventManager>().onCharacterEvent += HandleCharacterEvent;
        }

        private void Update()
        {
            float xAdjust = directionX * moveVelocity * TimeUtil.GetDelta();
            parentObject.transform.position = new Vector3(
                parentObject.transform.position.x + xAdjust, 
                parentObject.transform.position.y, 
                parentObject.transform.position.z);
        }

        private void HandleCharacterEvent(CharacterEvent ev)
        {
            if (ev.eventType != CharacterEventTypes.Movement)
            {
                return;
            }

            SetMoveDirection(ev.eventValue);
        }

        /// <summary>
        /// Sets the movement direction
        /// </summary>
        /// <param name="direction"></param>
        public void SetMoveDirection(float direction)
        {
            directionX = direction;
        }

        public void SetMovementVelocity(float velocity) { 
            moveVelocity = velocity;
        }
    }
}