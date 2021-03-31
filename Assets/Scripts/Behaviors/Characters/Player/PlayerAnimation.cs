using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    public bool playingDeathAnimation= false;

    public override void UpdateAnimator(Vector2 movementDirection)
    {
        base.UpdateAnimator(movementDirection);
    }

    public void PlayMovementAnimation()
    {
        animator.SetTrigger("ENABLE_MOVEMENT_ANIMATION");
    }

    public void PlayDeath() 
    {
        playingDeathAnimation = true;
        animator.SetTrigger("DEATH");
    }

    public void PlayCircle()
    {
        animator.SetTrigger("BACK_TO_CIRCLE");
    }

    private void DeathAnimationCompleted()
    {
        playingDeathAnimation = false;
    }
}
