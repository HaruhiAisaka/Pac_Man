using UnityEngine;

public class PinkyMovement : GhostMovement
{
    protected override void Awake()
    {
        base.Awake();
        currentMovementDirection = Vector2.left;
    }

    protected override void UpdateTargetChase()
    {
        target = (Vector2)pacMan.transform.position + 
            pacMan.playerMovement.GetCurrentViewingDirection() * Tile.tileSize * 4;
    }
}
