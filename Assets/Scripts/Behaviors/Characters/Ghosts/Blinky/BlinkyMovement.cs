using UnityEngine;

public class BlinkyMovement : GhostMovement
{
    protected override void Awake()
    {
        base.Awake();
        target = pacMan.transform.position;
        currentMovementDirection = Vector2.left;
    }

    protected override void UpdateTargetChase()
    {
        target = pacMan.transform.position;
    }
}