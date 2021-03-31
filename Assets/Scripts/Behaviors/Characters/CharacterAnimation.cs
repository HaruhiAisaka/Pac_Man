using UnityEngine;

public abstract class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool animationFrozen = false;
    public virtual void UpdateAnimator(Vector2 movementDirection)
    {
        if (movementDirection.Equals(Vector2.zero))
        {
            animator.speed = 0;
        }
        else if (!animationFrozen)
        {
            animator.speed = 1;
        }
        if (!movementDirection.Equals(Vector2.zero))
        {
            animator.SetFloat("XPos", movementDirection.x);
            animator.SetFloat("YPos", movementDirection.y);
        }
    }

    public void MakeCharacterInvisible()
    {
        animator.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void MakeCharacterVisible()
    {
        animator.enabled = true;
        spriteRenderer.enabled = true;
    }

    public void FreezeAnimation()
    {
        animationFrozen = true;
        animator.speed = 0;
    }

    public void UnfreezeAnimation()
    {
        animationFrozen = false;
        animator.speed = 1;
    }

}