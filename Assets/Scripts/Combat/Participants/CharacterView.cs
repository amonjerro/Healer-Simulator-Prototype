
using UnityEngine;

namespace Prototype
{
    public class CharacterView : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        Animator animator;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            CharacterEventManager.onCharacterEvent += ProcessEvent;
        }
        
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

        private void ProcessMove(float value)
        {
            animator.SetFloat("fVelocity", Mathf.Abs(value));
            if (value < 0)
            {
                spriteRenderer.flipX = true;
            } else
            {
                spriteRenderer.flipX = false;
            }
        }

    }
}
