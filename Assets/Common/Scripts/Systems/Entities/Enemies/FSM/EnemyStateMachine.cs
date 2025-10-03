using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStateMachine : EntityStatemachine  {
    
    [field: SerializeField] public float AgroProximity = 5f;
    [SerializeField] Transform _visionLight;
    protected override Dictionary<string, IStatemachineState> CreateStates() {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        PatrolBehaviour patrol = GetComponent<PatrolBehaviour>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        CacheBehaviour cache = GetComponent<CacheBehaviour>();
        MovementBehaviour movement = GetComponent<MovementBehaviour>();
        Animator animator = GetComponent<Animator>();
        FlipBehaviour flip = GetComponent<FlipBehaviour>();

        Dictionary<string, IStatemachineState> states = DefaultStates();

        states.Add("Patrol",    new EnemyPatrolState(this, patrol.Patrol, agent, renderer, movement.Movement));
        states.Add("Chase",     new EnemyChaseState(this, agent, flip.Flip, cache.Cache, movement.Movement, _walkAudio, animator, _visionLight));

        return states;
    }
}