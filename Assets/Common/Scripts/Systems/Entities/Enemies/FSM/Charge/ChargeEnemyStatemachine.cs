using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeEnemyStatemachine : EnemyStateMachine {
    [SerializeField] private LayerMask _walls;
    [field:SerializeField] public float BuildupDuration = 1f;
    [field:SerializeField] public float ChargeDistance { get; private set; } = 5f;
    [SerializeField] float _chargespeed = 0f;
    public bool CanCharge { get; private set; } = true;
    [SerializeField] float _chargeCooldown = 5f, _chargeCooldownTimer = 0f;

    protected override Dictionary<string, IStatemachineState> CreateStates() {
        Dictionary<string, IStatemachineState> states = base.CreateStates();

        CacheBehaviour cache = GetComponent<CacheBehaviour>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        MovementBehaviour move = GetComponent<MovementBehaviour>();

        ChargeEnemyBuildupState chargebuildupstate = new ChargeEnemyBuildupState(this, _walls, cache, agent, move);
        chargebuildupstate.OnChargeBuildup += () => CanCharge = false;

        states.Add("ChargeBuildup", chargebuildupstate);
        states.Add("Charge", new ChargeEnemyAttackState(this, _chargespeed, ChargeDistance, _walls, states["ChargeBuildup"] as ChargeEnemyBuildupState, move, cache));

        return states;
    }

    public override void Update() {
        base.Update();

        // if (CanCharge) {
        //     return;
        // }

        // if (CurrentState == "Charge" || CurrentState == "ChargeBuildup") {
        //     return;
        // }

        // if (_chargeCooldownTimer < _chargeCooldown) {
        //     _chargeCooldownTimer += Time.deltaTime;
        // }
        // else {
        //     CanCharge = true;
        // }
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions() {
        return new() {
            new StatemachineTrasition<string>("Idle", "Chase", () => States["Chase"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Idle", "Patrol", () => States["Patrol"].GetTransitionCondition() && !States["Idle"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Chase", "ChargeBuildup", () => States["Chase"].GetTransitionCondition() && States["ChargeBuildup"].GetTransitionCondition()),// && CanCharge),
            new StatemachineTrasition<string>("Chase", "Patrol", () =>  !States["Chase"].GetTransitionCondition() && States["Patrol"].GetTransitionCondition()),

            new StatemachineTrasition<string>("ChargeBuildup", "Charge", () => States["Chase"].GetTransitionCondition() && States["Charge"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Charge", "Idle", () => !States["Patrol"].GetTransitionCondition() && !(States["Charge"] as ChargeEnemyAttackState).Charging),

            new StatemachineTrasition<string>("Patrol", "Chase", () => States["Chase"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Patrol", "Idle", () => !States["Patrol"].GetTransitionCondition()),

            // new StatemachineTrasition<string>("Damage", "Chase", () => Animation2D.FinishedPlaying(_entity.Service.Component<Animator>(), "Hit") && States["Chase"].GetTransitionCondition()),
            // new StatemachineTrasition<string>("Damage", "Idle", () => Animation2D.FinishedPlaying(_entity.Service.Component<Animator>(), "Hit") && !States["Chase"].GetTransitionCondition()),
        };
    }

    protected override IStatemachineState Idle(Animator animator, AudioSource walkaudio) {
         return new EnemyIdleState(this, GetComponent<PatrolBehaviour>().Patrol, GameObject.FindGameObjectWithTag("Player"), GetComponent<CacheBehaviour>().Cache, animator, walkaudio);
    }
}