using UnityEngine;

public class PlayerIdleState : StatemachineState<PlayerStateMachine, string>, IStatemachineState{
    Animator _animator;
    AudioSource _walkAudio;
    GearBehaviour _gear;
    public PlayerIdleState(PlayerStateMachine statemachine, Animator animator, AudioSource walkaudio, GearBehaviour gear) : base(statemachine) {
        _animator = animator;
        _walkAudio = walkaudio;
        _gear = gear;  
    }

    public bool GetTransitionCondition() => false;

    public void OnAwake() {
    }

    public void Tick() {
    }

    public void TransitionEnter() {
        // if (_entity.Service<HealthService>().Dead) {
        //     return;
        // }
        _walkAudio.Stop();
        _animator.CrossFade("Idle", 0f);
        _gear.Gear.Animate("Idle", 0f);
    }

    public void TransitionExit() {

    }
}
