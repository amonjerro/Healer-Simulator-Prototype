using System.Runtime.CompilerServices;
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
        float activeMoveVelocity;
        Transform parentObject;

        /*
            Unity lifecycle 
        */
        private void Start()
        {
            parentObject = transform.parent;
            parentObject.GetComponent<CharacterEventManager>().onCharacterEvent += HandleCharacterEvent;
            activeMoveVelocity = moveVelocity;
        }

        private void Update()
        {
            float xAdjust = directionX * moveVelocity * TimeUtil.GetDelta();
            parentObject.transform.position = new Vector3(
                parentObject.transform.position.x + xAdjust, 
                parentObject.transform.position.y, 
                parentObject.transform.position.z);
        }

        /// <summary>
        /// Receive and manage character events
        /// </summary>
        /// <param name="ev">The event received</param>
        private void HandleCharacterEvent(CharacterEvent ev)
        {
            if (ev.eventType != CharacterEventTypes.Movement)
            {
                return;
            }

            SetMoveDirection((float) ev.EventValue);
        }

        /// <summary>
        /// Sets the movement direction
        /// </summary>
        /// <param name="direction"></param>
        public void SetMoveDirection(float direction)
        {
            directionX = direction;
        }

        /// <summary>
        /// Sets the movement velocity for this character
        /// </summary>
        /// <param name="velocity"></param>
        public void SetMovementVelocity(float velocity) { 
            moveVelocity = velocity;
        }
    }
}