using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStatemachine : Statemachine<string> {
    [SerializeField] protected AudioSource _gruntAudio;
    [SerializeField] protected AudioSource _walkAudio;
    [SerializeField] protected AudioSource _impactAudio;
    [SerializeField] protected LayerMask _wall;
        
    protected override List<StatemachineTrasition<string>> CreateAnyTransitions() => new() {
        new StatemachineTrasition<string>(null, "Damage", () => CurrentState != "Death" && States["Damage"].GetTransitionCondition()),
    };

    protected Dictionary<string, IStatemachineState> DefaultStates() {
        HealthComponent health = GetComponent<HealthBehaviour>().Health;
        CacheComponent cache = GetComponent<CacheBehaviour>().Cache;
        MovementComponent movement = GetComponent<MovementBehaviour>().Movement;
        Animator animator = GetComponent<Animator>();
        health.Death +=  () => ChangeState("Death"); 

        return new() { { "Idle", Idle(animator, _walkAudio) }, { "Damage", new EntityDamageState(this, cache, health, animator, _gruntAudio, _impactAudio, _wall) }, { "Death", new EntityDeathState(this, movement, animator) }
        };
    }

    public void TakeDamage(Vector2 origin, StatpointsComponent source) {
        (States["Damage"] as EntityDamageState).TakeDamage(origin, source);
    }

    protected abstract IStatemachineState Idle(Animator animator, AudioSource walkaudio);
}