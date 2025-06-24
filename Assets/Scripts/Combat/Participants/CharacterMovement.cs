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