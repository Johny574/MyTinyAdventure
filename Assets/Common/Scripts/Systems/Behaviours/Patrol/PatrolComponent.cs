using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolComponent : Component
{
    public List<PatrolPoint> PatrolPoints { get; set; } = new();
    public int _currentPoint = 0;

    public PatrolComponent(PatrolBehaviour behaviour, List<PatrolPoint> patrolPoints) : base(behaviour) {
        PatrolPoints = patrolPoints;
    }

    public void Initilize() {
        if (PatrolPoints.Count <= 0) {
            PatrolPoints = new() { new PatrolPoint(0f, Behaviour.transform.position) };
        }
    }


    public PatrolPoint CurrentPoint() => PatrolPoints[_currentPoint];

    public void SetNextPoint() => _currentPoint = (_currentPoint + 1) % PatrolPoints.Count;
}