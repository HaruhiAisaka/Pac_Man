using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData currentMapData;
    public TileDictionary tiles;
    public EdibleItemsOnBoard edibleItems;

    public MapData originalMap;
    public MapData killScreenMap;

    public void ResetLevel() 
    {
        edibleItems.Reset();
    }

    public Tile GetNextTile(Vector2 currentPosition, Vector2 direction)
    {
        return tiles.GetNextTile(currentPosition, direction);
    }

    public bool LevelComplete()
    {
        return edibleItems.AllPelletsEaten();
    }

    public void PlayStageClear()
    {
        tiles.PlayStageClearForAllTiles();
    }

    public void StopStageClearAnimation()
    {
        tiles.StopStageClearAnimation();
    }
}
