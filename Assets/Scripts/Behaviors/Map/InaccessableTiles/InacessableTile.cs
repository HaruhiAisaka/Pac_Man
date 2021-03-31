using UnityEngine;

public abstract class InacessableTile : Tile 
{
    public override void SetUpTile(TileDictionary tileDictionary)
    {
        // Debug.Log(gameObject.transform.position);
        this.tileDictionary = tileDictionary;
        SetSpriteBasedOnSuroundingTiles();
    }

    protected abstract void SetSpriteBasedOnSuroundingTiles();

    protected SuroundingTiles GetSuroundingTiles()
    {
        Tile[] suroundingTiles = new Tile[8];
        suroundingTiles[0] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(-1,1));
        suroundingTiles[1] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(0,1));
        suroundingTiles[2] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(1,1));
        suroundingTiles[3] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(-1,0));
        suroundingTiles[4] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(1,0));
        suroundingTiles[5] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(-1,-1));
        suroundingTiles[6] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(0,-1));
        suroundingTiles[7] = tileDictionary.GetNextTile(gameObject.transform.position, new Vector2(1,-1));
        return new SuroundingTiles(suroundingTiles);
    }

    public struct SuroundingTiles
    {
        public enum TileType {WallTile, NonWallTile, WarpTile, NonExistant, NA};
        public TileType[] suroundingTileTypes;

        public SuroundingTiles(TileType[] suroundingTileTypes)
        {
            this.suroundingTileTypes = suroundingTileTypes;
        }

        public SuroundingTiles(Tile[] tiles)
        {
            TileType[] suroundingTileTypes = new TileType[8];
            for (int i = 0; i < tiles.Length; i++)
            {
                suroundingTileTypes[i] = TileToTileType(tiles[i]);
            }
            this.suroundingTileTypes = suroundingTileTypes;
        }

        private static TileType TileToTileType(Tile tile)
        {
            if (tile == null)
            {
                return TileType.NonExistant;
            }
            else if (tile is InacessableTile)
            {
                return TileType.WallTile;
            }
            else if (tile is WarpTile)
            {
                return TileType.WarpTile;
            }
            else
            {
                return TileType.NonWallTile;
            }
        }

        // Returns rotation in degrees.
        public int GetRotation(SuroundingTiles suroundingTiles)
        {
            TileType[][] tileTypesToCheckAllRotations = 
                TileTypesAllFourRotations(suroundingTiles.suroundingTileTypes);
            int rotation = 0;
            foreach (TileType[] tileTypeRotationToCheck in tileTypesToCheckAllRotations)
            {
                if (TileTypeListEqual(suroundingTileTypes, tileTypeRotationToCheck))
                {
                    break;
                }
                rotation -= 90;
            }
            return rotation;
        }

        public bool EqualsInSomeRotation(SuroundingTiles suroundingTiles)
        {
            TileType[][] tileTypesToCheckAllRotations = 
                TileTypesAllFourRotations(suroundingTiles.suroundingTileTypes);
            bool result = false;
            foreach (TileType[] tileTypeRotationToCheck in tileTypesToCheckAllRotations)
            {
                result = result || TileTypeListEqual(suroundingTileTypes, tileTypeRotationToCheck);
            }
            return result;
        }

        private TileType[][] TileTypesAllFourRotations(TileType[] tileTypes)
        {
            TileType[] tileTypesRotation1 = 
                new TileType[] {
                    tileTypes[5],
                    tileTypes[3],
                    tileTypes[0],
                    tileTypes[6],
                    tileTypes[1],
                    tileTypes[7],
                    tileTypes[4],
                    tileTypes[2]
                };
            TileType[] tileTypesRotation2 =
                new TileType[] {
                    tileTypes[7],
                    tileTypes[6],
                    tileTypes[5],
                    tileTypes[4],
                    tileTypes[3],
                    tileTypes[2],
                    tileTypes[1],
                    tileTypes[0]
                };
            TileType[] tileTypesRotation3 =
                new TileType[] {
                    tileTypes[2],
                    tileTypes[4],
                    tileTypes[7],
                    tileTypes[1],
                    tileTypes[6],
                    tileTypes[0],
                    tileTypes[3],
                    tileTypes[5]
                };
            return new TileType[][] {
                tileTypes, 
                tileTypesRotation1, 
                tileTypesRotation2, 
                tileTypesRotation3
                };
        }

        private static bool TileTypeListEqual(TileType[] tileTypes1, TileType[] tileTypes2)
        {
            if (tileTypes1.Length != tileTypes2.Length)
            {
                return false;
            }

            bool result = true;
            for (int i = 0; i < tileTypes1.Length; i++)
            {
                result = TileTypeEqual(tileTypes1[i], tileTypes2[i]) && result;
            }

            return result;
        }

        private static bool TileTypeEqual(TileType tileType1, TileType tileType2)
        {
            if (tileType1 == TileType.NA || tileType2 == TileType.NA)
            {
                return true;
            }
            else
            {
                return tileType1 == tileType2;
            }
        }
    }
}