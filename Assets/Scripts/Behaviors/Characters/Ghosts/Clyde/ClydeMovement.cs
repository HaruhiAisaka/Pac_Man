using UnityEngine;

public class ClydeMovement : GhostMovement
{
    public float distanceUntilTargetChange;
    protected override void Awake()
    {
        base.Awake();
        currentMovementDirection = Vector2.left;
    }

    protected override void UpdateTargetChase()
    {
        if (Vector2.Distance(pacMan.transform.position, this.transform.position) 
        < distanceUntilTargetChange * Tile.tileSize) 
        {
            target = (Vector2)pacMan.transform.position + 
                pacMan.playerMovement.GetCurrentViewingDirection() * Tile.tileSize * 10;
        }
        else 
        {
            target = pacMan.transform.position;
        }
    }
}
