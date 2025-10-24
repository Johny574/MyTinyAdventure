
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAttackState : StatemachineState<RangedEnemyStatemachine, string>, IStatemachineState
{
    private LayerMask _walls;
    private float _attackTimer, _attackSpeed;
    float _attackRange;
    CacheBehaviour _cache;
    NavMeshAgent _agent;
    MovementBehaviour _movement;
    RangedBehaviour _ranged;
    public RangedEnemyAttackState(RangedEnemyStatemachine statemachine, CacheBehaviour cache, NavMeshAgent agent, MovementBehaviour move, RangedBehaviour ranged, float attackspeed = 5f, float attackRange = 5f) : base(statemachine)
    {
        _cache = cache;
        _agent = agent;
        _attackRange = attackRange;
        _movement = move;
        _ranged = ranged;
        _attackSpeed = attackspeed;
    }

    public bool GetTransitionCondition()
    {
        // if (_entity.Service<HealthService>().Dead) {
        //     return false;
        // }

        float distance = (float)Vector2.Distance(_statemachine.transform.position, _cache.Cache.CachedEntity.transform.position);

        Vector2 dif = (_cache.Cache.CachedEntity.transform.position - _statemachine.transform.position).normalized;
        RaycastHit2D wallhit = Physics2D.Raycast(_statemachine.transform.position, dif, distance, _walls);
        if (wallhit)
        {
            return false;
        }

        if (distance > _attackRange)
        {
            return false;
        }

        return true;
    }

    public void OnAwake()
    {

    }

    public void Tick()
    {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        if (_attackTimer < _attackSpeed)
        {
            _attackTimer += Time.deltaTime;
        }
        else
        {
            _attackTimer = 0f;
            Vector2 dif = (_cache.Cache.CachedEntity.transform.position - _statemachine.transform.position).normalized;
            // ProjectileLaunchData launchdata = new ProjectileLaunchData(_projectile, _projectileLater, dif, _statemachine.transform.position);
            // ProjectileFactory.Instance.Launch(launchdata);
            _ranged.Ranged.Fire(dif, _statemachine.transform.position);
            // new ProjectileCommand.LaunchCommand(_entity, _statemachine.transform.position, dif, _projectile, _projectileLater).Execute();
        }
    }

    public void TransitionEnter()
    {
        _attackTimer = 0;
        _agent.isStopped = true;
        // _entity.Component<Rigidbody2D>().linearVelocity = Vector2.zero;
        _movement.Movement.FrameInput = Vector2.zero;


        Vector2 dif = (_cache.Cache.CachedEntity.transform.position - _statemachine.transform.position).normalized;
        // new ProjectileCommand.LaunchCommand(_entity, _statemachine.transform.position, dif, _projectile, _enemyProjectileLayer).Execute();
    }

    public void TransitionExit()
    {

    }
}