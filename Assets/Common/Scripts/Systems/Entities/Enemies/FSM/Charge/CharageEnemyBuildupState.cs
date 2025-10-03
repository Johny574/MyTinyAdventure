using System;
using UnityEngine;


public class ChargeEnemyBuildupState : StatemachineState<ChargeEnemyStatemachine, string> {
    public float BuildupTimer = 0f;
    GameObject _entity;
    LayerMask _walls;
    public Action OnChargeBuildup;

    public ChargeEnemyBuildupState(ChargeEnemyStatemachine statemachine, GameObject entity, LayerMask walls) : base(statemachine) {

    }

    // public ChargeEnemyBuildupState(GameObject entity, ChargeEnemyStatemachine statemachine, LayerMask walls) {
    //     _entity = entity;
    //     _statemachine = statemachine;
    //     _walls = walls;
    // }

    public bool GetTransitionCondition() {
        // var cached =_entity.Service<CacheService>().Get<GameObject>();

        // if (cached == null) {
        //     return false;
        // }

        // if (Physics2D.Raycast(_entity.Component<Transform>().position, cached.Component<Transform>().position, _walls)) {
        //     return false;
        // }

        // return _statemachine.ChargeDistance < Vector2.Distance(_entity.Component<Transform>().position, cached.Component<Transform>().position);
        return true;
    }

    public void OnAwake() {

    }

    public void Tick() {
        // _entity.Component<Transform>().rotation = Quaternion.Euler(new Vector3(0f,0f,0f));

        // if (BuildupTimer < _statemachine.BuildupDuration) {
        //     BuildupTimer += Time.deltaTime;
        // }
    }

    public void TransitionEnter() {
        // BuildupTimer = 0f;
        // _entity.Component<NavMeshAgent>().isStopped = true;
        // OnChargeBuildup?.Invoke();
    }

    public void TransitionExit() {
        BuildupTimer = 0f;
    }
}