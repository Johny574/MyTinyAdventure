using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyStatemachine : EnemyStateMachine  {
    [SerializeField] private float _attackSpeed = 5f, _attackRange = 5f;
    [SerializeField] private WeaponItemSO.Type _attackType;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private LayerMask _walls;


    protected override Dictionary<string, IStatemachineState> CreateStates() {
        Dictionary<string, IStatemachineState> states = base.CreateStates();
        // states.Add("Attack", new RangedEnemyAttackState(_entity.Service, _walls, _attackSpeed, _attackRange, _attackType, _projectile));
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
        throw new System.NotImplementedException();
    }
}