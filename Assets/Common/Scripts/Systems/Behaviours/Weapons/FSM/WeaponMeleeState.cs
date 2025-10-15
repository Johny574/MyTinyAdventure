using UnityEngine;

public class WeaponMeleeState : StatemachineState<WeaponStatemachine, string>, IStatemachineState
{
    AimComponent _aim;
    GearComponent _gear;
    MeleeComponent _melee;
    HandsComponent _hands;

    public WeaponMeleeState(WeaponStatemachine statemachine, HandsComponent hands, GearComponent gear, AimComponent aim, MeleeComponent melee) : base(statemachine) {
        _statemachine = statemachine;
        _aim = aim;
        _gear = gear;
        _melee = melee;
        _hands = hands;
    }

    public bool GetTransitionCondition() {
        if (_gear.Gear[GearItemSO.Slot.Primary] == null || _gear.Gear[GearItemSO.Slot.Primary].Item == null)
            return false;
            
        return ((WeaponItemSO)_gear.Gear[GearItemSO.Slot.Primary].Item).DamageType.Equals(WeaponItemSO.Type.Melee);
    }

    public void OnAwake() {
        
    }

    public void Tick() {
        _melee.Tick();
        
        if (Input.GetMouseButtonDown(0)) {
            _melee.Attack(_aim.AimDelta);
        }
    }

    public void TransitionEnter() {
    }

    public void TransitionExit() {
      
    }
}