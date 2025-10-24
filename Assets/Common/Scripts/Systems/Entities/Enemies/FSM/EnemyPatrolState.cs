using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : StatemachineState<EnemyStateMachine, string>, IStatemachineState
{
    NavMeshPath _path = new();
    NavMeshAgent _agent;
    PatrolComponent _patrol;
    float _stoppingDistance;
    SpriteRenderer _renderer;
    MovementComponent _movement;
    GameObject _player;
    CacheBehaviour _cache;

    public EnemyPatrolState(EnemyStateMachine statemachine, PatrolComponent patrol, NavMeshAgent agent, SpriteRenderer renderer,  MovementComponent movement,  GameObject player, CacheBehaviour cache, float stoppingDistance = .1f) : base(statemachine) {
        _agent = agent;
        _patrol = patrol;
        _statemachine = statemachine;
        _stoppingDistance = stoppingDistance;
        _renderer = renderer;
        _movement = movement;
        _player = player;
        _cache = cache;
    }

    public bool GetTransitionCondition() {
        return Vector2.Distance(_patrol.CurrentPoint().Position, _agent.transform.position) > _stoppingDistance;
    }

    public void OnAwake() {
        // _patrolService.Remove(0);
    }

    public void Tick() {
        _agent.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        // KeyValuePair<int, List<PatrolPoint>> _patrolData = _patrolService.Get<KeyValuePair<int, List<PatrolPoint>>>();
        _renderer.flipX = Vector3.SignedAngle(_statemachine.transform.up, _agent.desiredVelocity, _statemachine.transform.forward) < 0 ? true : false;
        _agent.CalculatePath(_patrol.CurrentPoint().Position, _path);
        // _agent.SetPath(_path);
         _movement.FrameInput = (_statemachine.transform.position - _path.corners[0]).normalized;
          if (Vector2.Distance(_statemachine.transform.position, _player.transform.position) < _statemachine.AgroProximity) {
            _cache.Cache.Add(_player);
            Debug.Log("caching player");
        }
    }

    public void TransitionEnter() {
        // _entity.Component<Animator>().CrossFade("Run", 0f);
        // _entity.Service<AudioService>().AudioSettings["Walk"].PlayRandom();
    }

    public void TransitionExit() {
        // _patrolService.Add(1);
        // _entity.Service<AudioService>().AudioSettings["Walk"].Source.Stop();
    }
}