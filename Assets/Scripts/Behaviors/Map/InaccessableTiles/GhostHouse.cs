using System;
using UnityEngine;

public class GhostHouse : InacessableTile
{
    public bool doorSprite;
    public Sprite emptySpace, straightWall, convexCorner, door;
    protected override void SetSpriteBasedOnSuroundingTiles()
    {
        SuroundingTiles suroundingTiles = GetSuroundingTiles();
        SuroundingTiles suroundingTilesForEmptySpace = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile
                });
        SuroundingTiles suroundingTilesForStraightWall = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NA
                });
        SuroundingTiles suroundingTilesForConvexCorner = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile
                });

        if (doorSprite)
        {
            SetSprite(door);
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForEmptySpace))
        {
            SetSprite(emptySpace, suroundingTiles.GetRotation(suroundingTilesForEmptySpace));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForStraightWall))
        {
            SetSprite(straightWall, suroundingTiles.GetRotation(suroundingTilesForStraightWall));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForConvexCorner))
        {
            SetSprite(convexCorner, suroundingTiles.GetRotation(suroundingTilesForConvexCorner));
        }
        else
        {
            throw new InvalidOperationException("Unintended Possibility Discovered");
        }
    }
}
