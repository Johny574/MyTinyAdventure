using System.Collections.Generic;
using System.Linq;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour {
    public static TileGrid Instance;

    void Awake() {
        Instance = this; 
    }

    [field:SerializeField] public TilemapDictionary Tilemaps { get; private set; }

    public enum TilemapID {
        Floor,
        Wall,
        Props,
        Resources
    }

    public TileBase FloorTile, WallTile;
    public void PaintTiles(HashSet<Vector3Int> tiles, Tilemap map, TileBase tile) => map.SetTiles(tiles.ToArray(), Enumerable.Repeat(tile, tiles.Count).ToArray());
    public void ClearAll() {
        foreach (var tilemap in Tilemaps.Values) {
            tilemap.ClearAllTiles();   
        }
    }
}
