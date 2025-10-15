using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerStateMachine : EntityStatemachine  {
    Animator _animator;
    MovementBehaviour _movement;
    GearBehaviour _gear;

    void Awake() {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<MovementBehaviour>();
        _gear = GetComponent<GearBehaviour>();
     
        // InputManager.Instance.InputMappings["Skills"].
        // InputManager.Instance.InputMappings["Skills"].Action.action.performed += (test) => TakeDamage(aim.Aim.AimDelta, stats.Stats);
    }

    protected override Dictionary<string, IStatemachineState> CreateStates() {
        Dictionary<string, IStatemachineState> states = DefaultStates();
        states.Add("Move", new PlayerMoveState(this, _animator, _movement, _walkAudio, _gear));
        return states;
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions() {
        return new() {
            new StatemachineTrasition<string>("Idle", "Move",       States["Move"].GetTransitionCondition),
            new StatemachineTrasition<string>("Move", "Idle", () => !States["Move"].GetTransitionCondition()),
            new StatemachineTrasition<string>("Damage", "Idle", () => !States["Damage"].GetTransitionCondition()),
            // new StatemachineTrasition<string>("Damage", "Idle", () => Animation2D.FinishedPlaying(_entity.Service.Component<Animator>(), "Hit")),
        };
    }
    protected override IStatemachineState Idle(Animator animator, AudioSource walkaudio)     => new PlayerIdleState(this, _animator, _walkAudio, _gear);
}