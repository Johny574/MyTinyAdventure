
using UnityEngine;

public class MeleeEnemyAttackState : StatemachineState<MeleeEnemyStatemachine, string>, IStatemachineState
{
    MeleeComponent _melee;
    CacheComponent _cache;
    GearComponent _gear;
    FlipComponent _flip;
    public MeleeEnemyAttackState(MeleeEnemyStatemachine statemachine, MeleeComponent melee, CacheComponent cache, GearComponent gear, FlipComponent flip) : base(statemachine) {
        _melee = melee;
        _cache = cache;
        _gear = gear;
        _statemachine = statemachine;
        _flip = flip;
    }

    public bool GetTransitionCondition() {
        if (_gear.Gear[GearItemSO.Slot.Primary].Item.GetType() != typeof(MeleeWeaponItemData)) {
            // throw new System.Exception("Weapon was not a melee weapon on {_statemachine.name}. Please check the gear for this enemy.");
            Debug.Log(_gear.Gear[GearItemSO.Slot.Primary].GetType());
        }

        if (_melee.Attacking || !_statemachine.CanAttack) {
            return false;
        }
        
        return Vector2.Distance((Vector2)_cache.CachedEntity?.transform.position, _statemachine.transform.position) < ((MeleeWeaponItemData)_gear.Gear[GearItemSO.Slot.Primary].Item).Reach;
    }

    public void OnAwake() {

    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        var delta = _cache.CachedEntity.transform.position -  _statemachine.transform.position;
        Debug.DrawRay(_statemachine.transform.position, delta, Color.green);
        bool flipped = Vector3.SignedAngle(_statemachine.transform.up, delta, _statemachine.transform.forward) < 0 ? true : false;
        _flip.Flip(flipped);

        if (!_melee.Attacking && _statemachine.CanAttack) {
            Vector2 direction = (_cache.CachedEntity.transform.position - _statemachine.transform.position).normalized;
            _melee.Attack(direction);
            _statemachine.CanAttack = false;
        }
    }

    public void TransitionEnter() {
        
    }

    public void TransitionExit() {

    }
}