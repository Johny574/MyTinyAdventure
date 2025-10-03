using UnityEngine;

public class EntityDeathState : StatemachineState<EntityStatemachine, string> , IStatemachineState{
    MovementComponent _movement;
    Animator _animator;
    public EntityDeathState(EntityStatemachine statemachine, MovementComponent movement, Animator animator) : base(statemachine) {
        _movement = movement;
        _animator = animator;
    }

    // private EntityService _entity;

    // public EntityDeathState(EntityService entity) {
    //     _entity = entity;
    // }

    public bool GetTransitionCondition() {
        // return _entity.Service<HealthService>().Dead;
        return true;
    }

    public void OnAwake() {

    }

    public void Tick() {
        // _entity.Component<Transform>().rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
    }

    public void TransitionEnter() {
        _movement.CanMove = false;
        _animator.Play("Death");
        // _entity.Component<Animator>()?.Play("Death");

    }

    public void TransitionExit() {
    }
}