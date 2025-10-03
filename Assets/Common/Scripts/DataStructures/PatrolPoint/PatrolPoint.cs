
using System;
using UnityEngine;

[Serializable]
public struct PatrolPoint { 
    public float IdleTime;
    public Vector2 Position;

    public PatrolPoint(float idleTime, Vector2 idlePosition) {
        IdleTime = idleTime;
        Position = idlePosition;
    }
}