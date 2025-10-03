using UnityEngine;

public class EntityDamageState : StatemachineState<EntityStatemachine, string>, IStatemachineState
{
    public bool TookDamage { get; set; } = false;
    float  _damageforce = .2f;
    Vector2 _damageDelta;
    HealthComponent _health;
    Animator _animator;
    AudioSource _gruntAudio;
    AudioSource _impactAudio;
    LayerMask _wall;
    CacheComponent _cache;

    public EntityDamageState(EntityStatemachine statemachine, CacheComponent cache, HealthComponent health, Animator animator, AudioSource gruntAudio, AudioSource impactaudio, LayerMask wall) : base(statemachine) {
        _health = health;
        _animator = animator;
        _gruntAudio = gruntAudio;
        _impactAudio = impactaudio;
        _wall = wall;
        _cache = cache;
    }

    public bool GetTransitionCondition() => TookDamage;

    public void OnAwake() {
    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        if (!TookDamage)
            return;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Hit"))
            _animator.Play("Hit");

        if (stateInfo.normalizedTime < 1f) {
            RaycastHit2D hit = Physics2D.Raycast(_statemachine.transform.position, _damageDelta, _damageforce, _wall);
            if (!hit)
                _statemachine.transform.position += (Vector3)_damageDelta * _damageforce;
        }
        else
            TookDamage = false;
    }

    public void TransitionEnter() {
        _animator.Play("Hit");

        if (!_gruntAudio.isPlaying)
            _gruntAudio.Play();

        if (!_impactAudio.isPlaying)
            _impactAudio.Play();

        MainCamera.Instance.TriggerShake(.1f, 1f);

        // // Apply all buffs.
        // if (Source is IBuffSource) {
        //     foreach (var buff in (Source as IBuffSource).Buffs()) {
        //         _entity.Service.Service<BuffService>().Add(ResourceFactory.Buffs[buff.GetType()].Invoke(buff, _entity.Service));
        //     }
        // }
    }

    public void TransitionExit() {
        _animator.Play("Idle");
        TookDamage = false;
    }

    public void TakeDamage(Vector2 origin, StatpointsComponent source) {
        if (TookDamage)
            return;
            
        _health.Update(0 - source.StatPoints.AttackStats[StatPoints.Stat.Melee]);
        _damageDelta = ((Vector2)_statemachine.transform.position - origin).normalized;
        TookDamage = true;

        if (_cache.CachedEntity != source.Behaviour.gameObject)
            _cache.Add(source.Behaviour.gameObject);
    }
}