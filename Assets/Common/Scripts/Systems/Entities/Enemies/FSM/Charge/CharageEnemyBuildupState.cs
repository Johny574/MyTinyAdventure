using System;
using UnityEngine;
using UnityEngine.AI;


public class ChargeEnemyBuildupState : StatemachineState<ChargeEnemyStatemachine, string>, IStatemachineState {
    public float BuildupTimer = 0f;
    GameObject _entity;
    LayerMask _walls;
    public Action OnChargeBuildup;
    CacheBehaviour _cache;
    NavMeshAgent _agent;
    MovementBehaviour _movement;

    public ChargeEnemyBuildupState(ChargeEnemyStatemachine statemachine, LayerMask walls, CacheBehaviour cache, NavMeshAgent agent, MovementBehaviour move) : base(statemachine) {
        _movement = move;
        _cache = cache;
        _agent = agent;
    }

    public bool GetTransitionCondition() {
        var cached = _cache.Cache.CachedEntity;

        // if (cached == null) 
        //     return false;

        // if (Physics2D.Raycast(_statemachine.transform.position, cached.transform.position, _walls)) 
        //     return false;

        return Vector2.Distance(_statemachine.transform.position, _cache.Cache.CachedEntity.transform.position) <  _statemachine.ChargeDistance;
    }

    public void OnAwake() {

    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));

        if (BuildupTimer < _statemachine.BuildupDuration) 
            BuildupTimer += Time.deltaTime;
    }

    public void TransitionEnter() {
        BuildupTimer = 0f;
        _agent.isStopped = true;
        OnChargeBuildup?.Invoke();
        _movement.Movement.FrameInput = Vector2.zero;
    }

    public void TransitionExit() {
        BuildupTimer = 0f;
    }
}