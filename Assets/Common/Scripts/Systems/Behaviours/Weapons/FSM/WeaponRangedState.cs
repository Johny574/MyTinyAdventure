using UnityEngine;

public class WeaponRangedState : StatemachineState<WeaponStatemachine, string>, IStatemachineState
{
    GearComponent _gear;
    AimComponent _aim;
    HandsComponent _hands;
    RangedComponent _ranged;

    public WeaponRangedState(WeaponStatemachine statemachine, HandsComponent hands, GearComponent gear, AimComponent aim, RangedComponent ranged) : base(statemachine) {
        _aim = aim;
        _statemachine = statemachine;
        _gear = gear;
        _hands = hands;
        _ranged = ranged;
    }

    public bool GetTransitionCondition() {
        if (_gear.Gear[GearItemSO.Slot.Primary] == null || _gear.Gear[GearItemSO.Slot.Primary].Item == null)
            return false;

        return ((WeaponItemSO)_gear.Gear[GearItemSO.Slot.Primary].Item).DamageType.Equals(WeaponItemSO.Type.Ranged) || ((WeaponItemSO)_gear.Gear[GearItemSO.Slot.Primary].Item).DamageType.Equals(WeaponItemSO.Type.Magic);
    }

    public void OnAwake() {

    }

    public void Tick() {
        // _gear.Slots[GearItemSO.Slot.Primary].Object.transform.rotation = Quaterniozn.Euler(0, 0, Rotation2D.LookAngle(_aim.Aim));
        // _gear.Slots[GearItemSO.Slot.Primary].Object.transform.position = _statemachine.transform.position + Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle) * .5f;

        if (Input.GetMouseButtonDown(0))
            _ranged.Fire(_aim.AimDelta, _gear.Gear[GearItemSO.Slot.Primary].Object.transform.position);

        _gear.Gear[GearItemSO.Slot.Primary].Object.transform.rotation = Quaternion.Euler(0, 0, Rotation2D.LookAngle(_aim.AimDelta));
        _gear.Gear[GearItemSO.Slot.Primary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Primary].Object.transform.position, (Vector2)_statemachine.transform.position + _hands.PrimaryHandPosition + _aim.AimDelta * .4f, _hands.DampSpeed);
        _hands.PrimaryHand.transform.position =  (Vector2)_gear.Gear[GearItemSO.Slot.Primary].Object.transform.position - _aim.AimDelta * .2f;
        _gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position, (Vector2)_statemachine.transform.position + _hands.SecondaryHandPosition, _hands.DampSpeed); 
    }

    public void TransitionEnter() {
        // _trialRenderer.emitting = false;
    }

    public void TransitionExit() {
    }
}