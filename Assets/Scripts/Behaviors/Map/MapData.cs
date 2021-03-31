using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MapData : ScriptableObject
{
    public List<Vector2> pellets = new List<Vector2>();
    public List<Vector2> superPellet = new List<Vector2>();
    public List<Vector2> boarderTiles = new List<Vector2>();
    public List<Vector2> wallTiles = new List<Vector2>();
    public List<Vector2> acessableTiles = new List<Vector2>();
    public List<Vector2> warpTiles = new List<Vector2>();
    public List<Vector2> ghostHouseTiles = new List<Vector2>();
    public List<Vector2> acessableTilesNearGhostEnterace = new List<Vector2>();
    public List<Vector2> ghostHouseEnteranceTiles = new List<Vector2>();

    public MapData()
    {
        pellets = new List<Vector2>();
        superPellet = new List<Vector2>();
        boarderTiles = new List<Vector2>();
        wallTiles = new List<Vector2>();
        acessableTiles = new List<Vector2>();
        
    }
}
