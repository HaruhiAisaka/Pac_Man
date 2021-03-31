using System;
using UnityEngine;

public class BoarderTile : InacessableTile
{
    public Sprite straightWall, convexCorner, concaveCorner, concaveWall;
    protected override void SetSpriteBasedOnSuroundingTiles()
    {
        SuroundingTiles suroundingTiles = GetSuroundingTiles();
        SuroundingTiles suroundingTilesForStraightWall = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.NA,
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
                    SuroundingTiles.TileType.NA
                });
        SuroundingTiles suroundingTilesForConcaveCorner = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant
                });
        SuroundingTiles suroundingTilesForConcaveWall = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.NonWallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant
                });
        SuroundingTiles suroundingTilesForConcaveWallFlip =
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonExistant,
                    SuroundingTiles.TileType.WallTile,
                    SuroundingTiles.TileType.NonWallTile
                });
        SuroundingTiles isThereAWarpTileAdjacent = 
            new SuroundingTiles(
                new SuroundingTiles.TileType[] {
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.WarpTile,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NA,
                    SuroundingTiles.TileType.NA
                });
        if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForStraightWall))
        {
            SetSprite(straightWall, suroundingTiles.GetRotation(suroundingTilesForStraightWall));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForConcaveCorner))
        {
            SetSprite(concaveCorner, suroundingTiles.GetRotation(suroundingTilesForConcaveCorner));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForConvexCorner))
        {
            SetSprite(convexCorner, suroundingTiles.GetRotation(suroundingTilesForConvexCorner));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForConcaveWall))
        {
            SetSprite(concaveWall, suroundingTiles.GetRotation(suroundingTilesForConcaveWall));
        }
        else if (suroundingTiles.EqualsInSomeRotation(suroundingTilesForConcaveWallFlip))
        {
            SetSprite(concaveWall, suroundingTiles.GetRotation(suroundingTilesForConcaveWallFlip), flipX : true);
        }
        else if (suroundingTiles.EqualsInSomeRotation(isThereAWarpTileAdjacent))
        {
            SetSprite(straightWall, suroundingTiles.GetRotation(isThereAWarpTileAdjacent));
        }
        else
        {
            throw new InvalidOperationException("Unintended Possibility Discovered");
        }
    }
}