using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : StatemachineState<EnemyStateMachine, string>, IStatemachineState
{
    NavMeshPath _path = new(); 
    NavMeshAgent _agent;
    CacheComponent _cache;
    MovementComponent _movement;
    FlipComponent _flip;
    AudioSource _walkAudio;
    float _calculateTime = 0f;
    Animator _animator;
    Transform _vision;

    public EnemyChaseState(EnemyStateMachine statemachine, NavMeshAgent agent, FlipComponent flip, CacheComponent cache, MovementComponent movement, AudioSource walkaudio, Animator animator, Transform vision) : base(statemachine) {
        _flip = flip;
        _agent = agent;
        _cache = cache;
        _statemachine = statemachine;
        _walkAudio = walkaudio;
        _movement = movement;
        _animator = animator;
        _vision = vision;
    }

    public bool GetTransitionCondition() {
        // if (_entity.Service<HealthService>().Dead) {
        //     return false;
        // }

        return _cache.CachedEntity != null;
    }

    public void OnAwake() {
        
    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        if (_calculateTime < .5f)
            _calculateTime += Time.deltaTime;
        else
            _agent.CalculatePath(_cache.CachedEntity.transform.position, _path);

        if (_path.corners.Length <= 0)
            return;

        var delta = (_path.corners[1] - _statemachine.transform.position).normalized;
        _vision.rotation = Quaternion.Euler(0, 0, Rotation2D.LookAngle(delta));
        _movement.FrameInput = delta;
        bool flipped = Vector3.SignedAngle(_statemachine.transform.up, delta, _statemachine.transform.forward) < 0 ? true : false;
        _flip.Flip(flipped);
    }

    public void TransitionEnter() {
        _walkAudio.Play();
        _agent.CalculatePath(_cache.CachedEntity.transform.position, _path);
         _animator.CrossFade("Run", 0f);
    }

    public void TransitionExit()
    {
        _agent.isStopped = true;
        
    }
}
