using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyStatemachine : EnemyStateMachine {
    MeleeBehaviour _melee;

    public float AttackSpeed = 2f, AttackSpeedTimer = 0f; // use seperate timers then the one on meleeweapons otherwise the npc swings it like it wants to kill everything.
    public bool CanAttack = true;
   

    protected override List<StatemachineTrasition<string>> CreateTransitions() {
        return new() {
            new StatemachineTrasition<string>("Idle", "Chase",  () => States["Chase"].GetTransitionCondition()),
            // new StatemachineTrasition<string>("Idle", "Patrol", () => !States["Idle"].GetTransitionCondition() & States["Patrol"].GetTransitionCondition()),

            // new StatemachineTrasition<string>("Chase", "Patrol", () => !States["Chase"].GetTransitionCondition() && States["Patrol"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Damage", "Chase", () =>  States["Chase"].GetTransitionCondition()    && !States["Damage"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Chase", "Attack", () =>  States["Chase"].GetTransitionCondition()    && States["Attack"].GetTransitionCondition()),
            // new StatemachineTrasition<string>("Attack", "Chase", () =>  States["Chase"].GetTransitionCondition()    && States["Patrol"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Attack", "Patrol", () => !States["Chase"].GetTransitionCondition()   && States["Patrol"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Chase", "Patrol", () =>  !States["Chase"].GetTransitionCondition()    && States["Patrol"].GetTransitionCondition()),

            new StatemachineTrasition<string>("Attack", "Recover", () =>    States["Chase"].GetTransitionCondition()    && !CanAttack),
            new StatemachineTrasition<string>("Recover", "Chase", () =>     States["Chase"].GetTransitionCondition()    && CanAttack),

            new StatemachineTrasition<string>("Patrol", "Chase", () => States["Chase"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Patrol", "Idle", () => !States["Patrol"].GetTransitionCondition()),
        };
    }

    protected override Dictionary<string, IStatemachineState> CreateStates() {
        _melee = GetComponent<MeleeBehaviour>();
        CacheBehaviour  cache = GetComponent<CacheBehaviour>();
        GearBehaviour   gear = GetComponent<GearBehaviour>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        PatrolBehaviour patrol = GetComponent<PatrolBehaviour>();
        MovementBehaviour movement = GetComponent<MovementBehaviour>();
        FlipBehaviour flip = GetComponent<FlipBehaviour>();

        Dictionary<string, IStatemachineState> states = base.CreateStates();
        states.Add("Attack", new MeleeEnemyAttackState(this, _melee.Melee, cache.Cache, gear.Gear, flip.Flip));
        states.Add("Recover", new MeleeEnemyRecoverState(this, agent, patrol.Patrol, gear.Gear, movement.Movement, GameObject.FindGameObjectWithTag("Player")));
        return states;
    }

    public override void Update() {
        base.Update();
        if (AttackSpeedTimer < AttackSpeed && !CanAttack) {
            AttackSpeedTimer += Time.deltaTime;
        }
        else {
            AttackSpeedTimer = 0f;
            CanAttack = true;   
        }
        _melee.Melee.Tick();
    }

    protected override IStatemachineState Idle(Animator animator, AudioSource walkaudio) {
        return new EnemyIdleState(this, GetComponent<PatrolBehaviour>().Patrol, GameObject.FindGameObjectWithTag("Player"), GetComponent<CacheBehaviour>().Cache, animator, walkaudio);
    }
}