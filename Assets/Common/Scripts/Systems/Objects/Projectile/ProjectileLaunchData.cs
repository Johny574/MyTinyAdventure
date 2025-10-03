using System;
using UnityEngine;

[Serializable]
public struct ProjectileLaunchData
{
    public ProjectileSO Variant;
    public LayerMask Layer;
    public Vector2 Direction;
    public Vector2 Position;

    public ProjectileLaunchData(ProjectileSO variant, LayerMask layer, Vector2 direction, Vector2 position) {
        Variant = variant;
        Layer = layer;
        Direction = direction;
        Position = position;
    }
}