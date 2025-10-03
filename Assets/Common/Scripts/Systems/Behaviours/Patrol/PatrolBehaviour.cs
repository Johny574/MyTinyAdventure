



using System.Collections.Generic;
using UnityEngine;
public class PatrolBehaviour : MonoBehaviour
{
    public PatrolComponent Patrol { get; set; }
    [SerializeField] List<PatrolPoint> _patrolPoints = new();
    void Awake() {
        Patrol = new(this, _patrolPoints);
    }


    void Start() {
        Patrol.Initilize();
    }
    
}