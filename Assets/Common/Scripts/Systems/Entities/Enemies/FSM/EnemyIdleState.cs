using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StatemachineState<EnemyStateMachine, string>, IStatemachineState{
    float _idleTimer = 0f;
    PatrolComponent _patrol;
    GameObject _player;
    CacheComponent _cache;
    Animator _animator;
    AudioSource _walkAudio;

    public EnemyIdleState(EnemyStateMachine statemachine, PatrolComponent patrol, GameObject player, CacheComponent cache, Animator animator, AudioSource walkaudio) : base(statemachine) {
        _patrol = patrol;
        _statemachine = statemachine;
        _player = player;
        _cache = cache;
        _animator = animator;
        _walkAudio = walkaudio;
    }

    public bool GetTransitionCondition() {
        return _idleTimer < _patrol.CurrentPoint().IdleTime;
    }

    public void OnAwake() {
    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        _idleTimer += Time.deltaTime;
        if (Vector2.Distance(_statemachine.transform.position, _player.transform.position) < _statemachine.AgroProximity) {
            _cache.Add(_player);
        }
    }

    public void TransitionEnter() {
        // _entity.Component<Animator>().CrossFade("Idle", 0f);
        _walkAudio.Stop();
        _animator.CrossFade("Idle", 0f);
    }

    public void TransitionExit() {
        _idleTimer = 0f;
    }
}