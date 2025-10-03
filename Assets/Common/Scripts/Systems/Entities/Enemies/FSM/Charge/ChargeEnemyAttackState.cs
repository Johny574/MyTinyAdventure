using UnityEngine;

public class ChargeEnemyAttackState : StatemachineState<ChargeEnemyStatemachine, string> {
    private GameObject _entity;
    float _chargespeed , _distance, _currentDistance;
    Vector2 _direction, _start;
    private LayerMask _walls;
    ChargeEnemyBuildupState _buildupState;
    public bool Charging = false;

    public ChargeEnemyAttackState(ChargeEnemyStatemachine statemachine, GameObject entity, float chargespeed, float distance, LayerMask walls, ChargeEnemyBuildupState buildupState) : base(statemachine) {
        _entity = entity;
        _chargespeed = chargespeed;
        _distance = distance;
        _statemachine = statemachine;
        _walls = walls;
        _buildupState = buildupState;

    }
    public bool GetTransitionCondition() => _buildupState.BuildupTimer > _statemachine.BuildupDuration;

    public void OnAwake() {

    }

    public void Tick() {
        // _entity.Component<Transform>().rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        // _currentDistance = Vector2.Distance(_start, (Vector2)_entity.Component<Transform>().position);

        // if (_currentDistance > _distance) {
        //     _statemachine.ChangeState("Idle");
        //     Charging = false;
        // }

        // _entity.Service<MovementService>().Set(_direction);
        // Charging = true;
    }

    public void TransitionEnter() {
        // _direction = (Vector2)(_entity.Service<CacheService>().Get<EntityService>().Component<Transform>().position - _entity.Component<Transform>().position).normalized;
        // _start = (Vector2)_entity.Component<Transform>().position;
        // Charging = true;
        // _entity.Service<MovementService>().Add(_chargespeed);
    }

    public void TransitionExit() {
        // _entity.Service<MovementService>().Remove(_chargespeed);
    }
}