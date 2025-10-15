
using UnityEngine;

public class RangedEnemyAttackState : StatemachineState<RangedEnemyStatemachine, string>, IStatemachineState {
    // private EntityService _entity;
    private LayerMask _walls;
    private float _attackTimer, _attackSpeed;
    private Projectile _projectile;
    private WeaponItemSO.Type _attackType;
    private LayerMask _enemyProjectileLayer;

    private float _attackRange;

    public RangedEnemyAttackState(RangedEnemyStatemachine statemachine) : base(statemachine) {
    }

    // public RangedEnemyAttackState(EntityService entity, LayerMask walls, float attackspeed, float attackRange, WeaponItemData.Type attackType, Projectile projectile = null) {
    //     _entity = entity;
    //     _attackSpeed = attackspeed;
    //     _attackRange = attackRange;
    //     _projectile = projectile;
    //     _walls = walls;
    //     _attackType = attackType;
    //     _enemyProjectileLayer = LayerMask.NameToLayer("EProjectile");

    // }

    public bool GetTransitionCondition() {
        // if (_entity.Service<HealthService>().Dead) {
        //     return false;
        // }
        
        // float distance = (float)Vector2.Distance(_entity.Component<Transform>().position, _entity.Service<CacheService>().Get<EntityService>().Component<Transform>().position);

        // Vector2 dif = (_entity.Service<CacheService>().Get<EntityService>().Component<Transform>().position - _entity.Component<Transform>().position).normalized;
        // RaycastHit2D wallhit = Physics2D.Raycast(_entity.Component<Transform>().position, dif, distance, _walls);
        // if (wallhit) {
        //     return false;
        // }

        // if (distance > _attackRange) {
        //     return false;            
        // }
        
        return true;
    }

    public void OnAwake() {

    }

    public void Tick() {
        // _entity.Component<Transform>().rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        // if (_attackTimer < _attackSpeed) {
        //     _attackTimer += Time.deltaTime;
        // }
        // else {
        //     _attackTimer = 0f;
        //     Vector2 dif = (_entity.Service<CacheService>().Get<EntityService>().Component<Transform>().position - _entity.Component<Transform>().position).normalized;
        //     if (_attackType == WeaponItemData.Type.Ranged) {
        //         new ProjectileCommand.LaunchCommand(_entity, _entity.Component<Transform>().position, dif, _projectile, _enemyProjectileLayer).Execute();
        //     }
        // }
    }

    public void TransitionEnter() {
        // _attackTimer = 0;
        // _entity.Component<NavMeshAgent>().isStopped = true;
        // _entity.Component<Rigidbody2D>().linearVelocity = Vector2.zero;
        
        // if (_attackType == WeaponItemData.Type.Melee) {
        //     return;
        // }

        // Vector2 dif = (_entity.Service<CacheService>().Get<EntityService>().Component<Transform>().position - _entity.Component<Transform>().position).normalized;
        // new ProjectileCommand.LaunchCommand(_entity, _entity.Component<Transform>().position, dif, _projectile, _enemyProjectileLayer).Execute();
    }

    public void TransitionExit() {
        
    }
}