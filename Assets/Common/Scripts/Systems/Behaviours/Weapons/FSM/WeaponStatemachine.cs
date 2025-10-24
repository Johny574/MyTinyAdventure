

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimBehaviour))]
[RequireComponent(typeof(GearBehaviour))]
public class WeaponStatemachine : Statemachine<string>
{
    HealthComponent _health;
    LayerMask _enemyLayer;
    public bool CanAttack = true;
    [SerializeField] AudioSource _swingAudio;
    protected override List<StatemachineTrasition<string>> CreateAnyTransitions() {
        return new() {
            new StatemachineTrasition<string>(null, "Default",   States["Default"].GetTransitionCondition),
            new StatemachineTrasition<string>(null, "Melee",     States["Melee"].GetTransitionCondition),
            new StatemachineTrasition<string>(null, "Ranged",    States["Ranged"].GetTransitionCondition),
        };
    }

    protected override Dictionary<string, IStatemachineState> CreateStates() {
        GearBehaviour gear = GetComponent<GearBehaviour>();
        AimBehaviour aim = GetComponent<AimBehaviour>();
        RangedBehaviour ranged = GetComponent<RangedBehaviour>();
        MeleeBehaviour melee = GetComponent<MeleeBehaviour>();
        HandsBehaviour hands = GetComponent<HandsBehaviour>();
        StatpointsBehaviour stats = GetComponent<StatpointsBehaviour>();
        _health = GetComponent<HealthBehaviour>().Health;

        return new() {
            { "Default",    new WeaponDefaultState(this, hands.Hands, gear.Gear, aim.Aim, _enemyLayer, stats, _swingAudio)},
            { "Ranged",     new WeaponRangedState(this, hands.Hands, gear.Gear, aim.Aim, ranged.Ranged) },
            { "Melee",      new WeaponMeleeState(this, hands.Hands, gear.Gear, aim.Aim, melee.Melee) }
        };
    }

    public override void Update() {
        if (!_health.Dead)
            base.Update();
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions() {
        return new();
    }
}