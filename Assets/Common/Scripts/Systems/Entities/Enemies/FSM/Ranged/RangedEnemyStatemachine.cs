using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyStatemachine : EnemyStateMachine  {
    [SerializeField] private float _attackSpeed = 5f, _attackRange = 5f;
    protected override Dictionary<string, IStatemachineState> CreateStates() {
        Dictionary<string, IStatemachineState> states = base.CreateStates();
        CacheBehaviour cache = GetComponent<CacheBehaviour>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        RangedBehaviour ranged = GetComponent<RangedBehaviour>();
        MovementBehaviour move = GetComponent<MovementBehaviour>();
        states.Add("Attack", new RangedEnemyAttackState(this, cache, agent, move, ranged, _attackSpeed, _attackRange));
        return states;
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions() {
        return new() {
            new StatemachineTrasition<string>("Idle", "Chase",      () => States["Chase"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Idle", "Patrol",     () => States["Patrol"].GetTransitionCondition() && !States["Idle"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Chase", "Attack",    () => States["Chase"].GetTransitionCondition() && States["Attack"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Chase", "Patrol",    () => !States["Chase"].GetTransitionCondition() && States["Patrol"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Attack", "Chase",    () => !States["Attack"].GetTransitionCondition() && States["Chase"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Patrol", "Chase",    () => States["Chase"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Patrol", "Idle",     () => !States["Patrol"].GetTransitionCondition()),

            // new StatemachineTrasition<string>("Damage", "Chase", () => Animation2D.FinishedPlaying(_entity.Service.Component<Animator>(), "Hit") && States["Chase"].GetTransitionCondition()),
            // new StatemachineTrasition<string>("Damage", "Idle", () => Animation2D.FinishedPlaying(_entity.Service.Component<Animator>(), "Hit") && !States["Chase"].GetTransitionCondition()),
        };
    }



    protected override IStatemachineState Idle(Animator animator, AudioSource walkaudio) {
        return new EnemyIdleState(this, GetComponent<PatrolBehaviour>().Patrol, GameObject.FindGameObjectWithTag("Player"), GetComponent<CacheBehaviour>().Cache, animator, walkaudio);
    }
}