using System;
using UnityEngine;

namespace Prototype
{
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

        public void SetMoveVector(Vector2 moveVector)
        {
            directionX = moveVector.x;
            CharacterEvent ev = new CharacterEvent(CharacterEventTypes.Movement, directionX);
            CharacterEventManager.BroadcastCharacterEvent(ev);
        }

        public void SetMovementVelocity(float velocity) { 
            moveVelocity = velocity;
        }
    }
}