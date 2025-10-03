using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TileGrid _tilegrid = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    public void ClearAll() => _tilegrid.ClearAll();

    public void GenerateDungeon() {
        _tilegrid.ClearAll();
        GenerateFloor();
        GenerateWalls();
    }

    public void GenerateFloor() => CreateFloors(_tilegrid.Tilemaps[TileGrid.TilemapID.Floor], _tilegrid.FloorTile);
    public void GenerateWalls() => CreateWalls(_tilegrid.Tilemaps[TileGrid.TilemapID.Floor], _tilegrid.Tilemaps[TileGrid.TilemapID.Wall], _tilegrid.WallTile);
    protected abstract void CreateFloors(Tilemap floormap, TileBase floortile);
    protected abstract void CreateWalls(Tilemap floormap, Tilemap wallmap, TileBase walltile);
}