using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [Header("Other Game Objects")]
    public Ghosts ghosts;
    private Player player;

    [Header("Movement Options")]
    public float normalSpeedFactor;
    public float eatingSpeedFactor;
    public float normalFrightenedSpeedFactor;
    public float eatingFrightenedSpeedFactor;
    public float corneringRadius;
    private Vector2 inputDirection;
    
    protected override void Awake()
    {
        base.Awake();
        currentMovementDirection = Vector2.left;
        player = GetComponent<Player>();
    }
    
    public void UpdateInputDirection(Vector2 vector)
    {
        // Make sure that movement direction is only updated if it is a
        // vector that goes in the four cardinal directions or if it is zero.
        if (vector.x == 0 || vector.y == 0)
        {
            inputDirection = vector;
        }
    }

    public Vector2 GetCurrentViewingDirection()
    {
        return currentViewingDirection;
    }

    protected override bool UpdateCurrentMovementDirection()
    {
        bool update = !transformLerpRunning || OppositeVectors(currentMovementDirection, inputDirection);
        if (update)
        {
            if (CanMoveInDirection(inputDirection))
            {
                currentMovementDirection = inputDirection;
            }
            else if (!CanMoveInDirection(currentMovementDirection) && !(currentTile is WarpTile))
            {
                currentMovementDirection = Vector2.zero;
            }
            
            player.playerInteraction.eating = false;
        }
        if (!currentMovementDirection.Equals(Vector2.zero))
        {
            currentViewingDirection = currentMovementDirection;
        }
        return update;
    }
    
    protected override void UpdateSpeed()
    {
        if (player.playerInteraction.eating && ghosts.ghostFrightened)
        {
            speed = CalculateSpeed(eatingFrightenedSpeedFactor);
        }
        else if (player.playerInteraction.eating)
        {
            speed = CalculateSpeed(eatingSpeedFactor);
        }
        else if (ghosts.ghostFrightened)
        {
            speed = CalculateSpeed(normalFrightenedSpeedFactor);
        }
        else
        {
            speed = CalculateSpeed(normalSpeedFactor);
        }
    }

    public void ResetInitialMovementDirection()
    {
        currentMovementDirection = Vector2.left;
    }
}
