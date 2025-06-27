
using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Class concerned with all visual behavior for a particular character.
    /// </summary>
    public class CharacterView : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Specifies whether this character should turn around when backing up")]
        bool shouldMoonwalk;

        [SerializeField]
        [Tooltip("Specifies whether this character begins combat looking right")]
        bool startsFlipped;

        // Component references
        SpriteRenderer spriteRenderer;
        Animator animator;

        bool movementStopped = false;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            GetComponentInParent<CharacterEventManager>().onCharacterEvent += ProcessEvent;
        }
        
        /// <summary>
        /// Process gameplay events to determine the corresponding visuals
        /// </summary>
        /// <param name="e"></param>
        private void ProcessEvent(CharacterEvent e)
        {
            switch (e.eventType) {
                case CharacterEventTypes.Movement:
                    ProcessMove((float)e.EventValue);
                    break;
                default:
                    return;
            }
        }


        /// <summary>
        /// This processes the movement events for this character.
        /// This triggers animations, flips the sprite renderer, you name it.
        /// </summary>
        /// <param name="value"></param>
        private void ProcessMove(float value)
        {
            if (movementStopped) return;

            animator.SetFloat("fVelocity", Mathf.Abs(value));
            if (value < 0)
            {
                spriteRenderer.flipX = !startsFlipped;
            } else
            {
                spriteRenderer.flipX = startsFlipped;
            }
        }

        public Sprite GetSprite()
        {
            return spriteRenderer.sprite;
        }


    }
}
