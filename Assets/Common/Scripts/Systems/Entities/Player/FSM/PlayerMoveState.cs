using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : StatemachineState<PlayerStateMachine, string>, IStatemachineState{
    Animator _animator;
    MovementBehaviour _movement;
    AudioSource _walkAudio;
    


    public PlayerMoveState(PlayerStateMachine statemachine, Animator animator, MovementBehaviour movementComponent, AudioSource walkaudio) : base(statemachine) {
        _animator = animator;
        _movement = movementComponent;
        _walkAudio = walkaudio;
    }

    public bool GetTransitionCondition() => InputManager.Instance.InputMappings["Move"].Action.action.ReadValue<Vector2>() != Vector2.zero;

    public void OnAwake() {
        
    }

    public void Tick() {
        _movement.Movement.FrameInput = InputManager.Instance.InputMappings["Move"].Action.action.ReadValue<Vector2>();

        if (InputManager.Instance.InputMappings["Sprint"].Action.action.WasPressedThisFrame() && _movement.Movement.Stamina.CanSprint) {
            _movement.Movement.Stamina.Sprinting = true;
        }
        else if (InputManager.Instance.InputMappings["Sprint"].Action.action.WasReleasedThisFrame() || !_movement.Movement.Stamina.CanSprint) {
            if (_movement.Movement.Stamina.Sprinting)
                _movement.Movement.Stamina.Sprinting = false;
        }
    }

    public void TransitionEnter() {
        // if (_entity.Service<HealthService>().Dead) {
        //     return;
        // }
        _walkAudio.Play();
        _animator.CrossFade("Run", 0f);
    }

    public void TransitionExit() {
        _movement.Movement.FrameInput = Vector2.zero;
        // _entity.Service<AudioService>().AudioSettings["Walk"].Source.Stop();
    }
}