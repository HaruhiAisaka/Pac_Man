using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTile : AccessableTile
{
    public WarpTile connectingWarpTile;
    public Vector2 directionToWarp;

    public void WarpObject(GameObject gameObject)
    {
        gameObject.transform.position = connectingWarpTile.transform.position;
    }
}
