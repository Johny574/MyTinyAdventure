using System;
using UnityEngine;

[Serializable]
public struct ProjectileLaunchData
{
    public ProjectileSO Variant;
    public int Target;
    public Vector2 Direction;
    public Vector2 Position;
    public StatpointsComponent Stats;

    public ProjectileLaunchData(ProjectileSO variant, LayerMask target, Vector2 direction, Vector2 position, StatpointsComponent stats)
    {
        Variant = variant;
        Target = Mathf.RoundToInt(Mathf.Log(target.value, 2));
        Direction = direction;
        Position = position;
        Stats = stats;
    }
}