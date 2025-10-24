using UnityEngine;

public class ChargeEnemyAttackState : StatemachineState<ChargeEnemyStatemachine, string>, IStatemachineState {
    float _chargespeed , _distance, _currentDistance;
    Vector2 _direction, _start;
    private LayerMask _walls;
    ChargeEnemyBuildupState _buildupState;
    public bool Charging = false;
    MovementBehaviour _movement;
    CacheBehaviour _cache;

    public ChargeEnemyAttackState(ChargeEnemyStatemachine statemachine, float chargespeed, float distance, LayerMask walls, ChargeEnemyBuildupState buildupState, MovementBehaviour movement, CacheBehaviour cache) : base(statemachine) {
        _chargespeed = chargespeed;
        _distance = distance;
        _statemachine = statemachine;
        _walls = walls;
        _buildupState = buildupState;
        _cache = cache;
        _movement = movement;
    }
    public bool GetTransitionCondition() => _buildupState.BuildupTimer > _statemachine.BuildupDuration;

    public void OnAwake() {

    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        _currentDistance = Vector2.Distance(_start, (Vector2)_statemachine.transform.position);

        if (_currentDistance > _distance || Physics2D.Raycast(_statemachine.transform.position, _direction, 2f, _walls)) {
            _statemachine.ChangeState("Idle");
            Charging = false;
        }
    }

    public void TransitionEnter() {
        _direction = (Vector2)(_cache.Cache.CachedEntity.transform.position - _statemachine.transform.position).normalized;
        _start = (Vector2)_statemachine.transform.position;
        _movement.Movement.FrameInput = _direction;
        _movement.Movement.CurrentSpeed = _chargespeed;
        Charging = true;
        // _entity.Service<MovementService>().Add(_chargespeed);
    }

    public void TransitionExit() {
        // _entity.Service<MovementService>().Remove(_chargespeed);
        _movement.Movement.CurrentSpeed = 1f;
        _movement.Movement.FrameInput = Vector2.zero;
    }
}