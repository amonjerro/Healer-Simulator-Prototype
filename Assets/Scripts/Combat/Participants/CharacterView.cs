
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

        [SerializeField]
        GameObject floatingText;

        // Component references
        SpriteRenderer spriteRenderer;
        Animator animator;

        bool movementStopped = false;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            GetComponentInParent<CharacterEventManager>().onCharacterEvent += ProcessEvent;
            GetComponentInParent<CharacterEventManager>().onStatusChange += ProcessEvent;

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
                case CharacterEventTypes.DamageTaken:
                    ProcessHealthChanged((int)e.EventValue);
                    break;
                case CharacterEventTypes.Death:
                    ProcessDeath((int)e.EventValue);
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

        /// <summary>
        /// Show to the player that this character's health has changed in some capacity
        /// </summary>
        /// <param name="value">The value delta</param>
        private void ProcessHealthChanged(int value)
        {
            ShowFloatingText(value);
            if (value < 0)
            {
                animator.SetTrigger("DamageTaken");
            }
        }


        private void ProcessDeath(int value)
        {
            ShowFloatingText(value);
            animator.SetTrigger("tDie");
        }

        private void ShowFloatingText(int value)
        {
            GameObject text = Instantiate(floatingText, transform);
            TextMesh textMesh = text.GetComponent<TextMesh>();
            textMesh.text = Mathf.Abs(value).ToString();
            if (value > 0)
            {
                textMesh.color = Color.green;
            }
        }

        /// <summary>
        /// Get this characters' sprite
        /// </summary>
        /// <returns>The sprite</returns>
        public Sprite GetSprite()
        {
            return spriteRenderer.sprite;
        }
    }
}
