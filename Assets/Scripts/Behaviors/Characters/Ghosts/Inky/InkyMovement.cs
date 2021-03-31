using UnityEngine;

public class InkyMovement : GhostMovement
{
    public BlinkyMovement blinky;
    protected override void Awake()
    {
        base.Awake();
        currentMovementDirection = Vector2.left;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void UpdateTargetChase()
    {
        Vector2 pacManTarget = 
            (Vector2)pacMan.transform.position + 
            pacMan.playerMovement.GetCurrentViewingDirection() * Tile.tileSize * 2;
        
        Vector2 blinkyToPacManTarget = pacManTarget - (Vector2)blinky.transform.position;
        target = pacManTarget + blinkyToPacManTarget;
    }
}
