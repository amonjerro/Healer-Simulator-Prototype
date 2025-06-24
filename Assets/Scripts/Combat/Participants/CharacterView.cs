
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

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            CharacterEventManager.onCharacterEvent += ProcessEvent;
        }
        
        /// <summary>
        /// Process gameplay events to determine the corresponding visuals
        /// </summary>
        /// <param name="e"></param>
        private void ProcessEvent(CharacterEvent e)
        {
            switch (e.eventType) {
                case CharacterEventTypes.Movement:
                    ProcessMove(e.eventValue);
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
            animator.SetFloat("fVelocity", Mathf.Abs(value));
            if (value < 0)
            {
                spriteRenderer.flipX = !startsFlipped;
            } else
            {
                spriteRenderer.flipX = startsFlipped;
            }
        }

    }
}
