using UnityEngine;

public class PlayerIdleState : StatemachineState<PlayerStateMachine, string>, IStatemachineState{
    Animator _animator;
    AudioSource _walkAudio;


    public PlayerIdleState(PlayerStateMachine statemachine, Animator animator, AudioSource walkaudio) : base(statemachine) {
        _animator = animator;
        _walkAudio = walkaudio;
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
    }

    public void TransitionExit() {

    }
}
