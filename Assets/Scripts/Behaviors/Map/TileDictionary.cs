using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileDictionary : MonoBehaviour
{
    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    public List<Vector2> ghostHouseEnterenceTiles = new List<Vector2>();

    public BoarderTile boarderTilePrefab;
    public WallTile wallTilePrefab;
    public WarpTile warpTilePrefab;
    public AccessableTile acessableTilePrefab;
    public GhostHouse ghostHouseTilePrefab;

    public bool presetTiles;

    void Start()
    {
        if (presetTiles)
        {
            SetDictionaryPresetTiles();
        }
    }

    public void InstantiateTiles(MapData mapData)
    {
        tiles.Clear();
        InstantiateTilesOfCertainType(mapData.acessableTiles, acessableTilePrefab);
        InstantiateTilesOfCertainType(mapData.boarderTiles, boarderTilePrefab);
        InstantiateTilesOfCertainType(mapData.wallTiles, wallTilePrefab);
        InstantiateTilesOfCertainType(mapData.warpTiles, warpTilePrefab);
        InstantiateTilesOfCertainType(mapData.ghostHouseTiles, ghostHouseTilePrefab);
    }

    private void InstantiateTilesOfCertainType(IList<Vector2> positions, Tile tilePrefab) 
    {
        foreach (Vector2 position in positions)
        {
            InstantiateTile(position, tilePrefab);
        }
    }

    private void InstantiateTile(Vector2 position, Tile tilePrefab)
    {
        Tile tile = Instantiate(tilePrefab, this.transform);
        tile.transform.localPosition = position;
        tiles.Add(position, tile);
    }

    public Tile GetTile(Vector2 position) 
    {
        if (tiles.ContainsKey(position)) 
        {
            return tiles[position];
        }
        else 
        {
            throw new System.ArgumentException("Position doesn't exist");
        }
    }

    public bool TileExists(Vector2 position)
    {
        return tiles.ContainsKey(position);
    }

    void SetDictionaryPresetTiles()
    {
        foreach (Tile tile in this.GetComponentsInChildren<Tile>())
        {
            tiles[tile.gameObject.transform.position] = tile;
        }
        foreach (Tile tile in tiles.Values)
        {
            tile.SetUpTile(this);
        }
    }

    public void PlayStageClearForAllTiles()
    {
        foreach (Tile tile in tiles.Values)
        {
            tile.PlayStageClear();
        }
    }

    public void StopStageClearAnimation()
    {
        foreach (Tile tile in tiles.Values)
        {
            tile.StopStageClearAnimation();
        }
    }
    
    public Tile GetNextTile(Vector2 currentPosition, Vector2 direction)
    {
        direction.Normalize();
        direction = direction * Tile.tileSize;
        // Tiles are on either X.25 or X.75 plus or minus. 
        // Using this fact, we can determine the next tile
        // from any position, given the direction.
        Vector2 nextTile = currentPosition + direction;
        nextTile = Tile.RoundToNearestTilePos(nextTile);
        if (tiles.ContainsKey(nextTile))
        {
            return tiles[nextTile];
        }
        else
        {
            return null;
        }
    }
}