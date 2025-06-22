using UnityEngine;

public class CharacterView : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        animator = GetComponent<Animator>();
    }
}