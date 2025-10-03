using System.Collections.Generic;
using UnityEngine;

public class WeaponDefaultState : StatemachineState<WeaponStatemachine, string>, IStatemachineState
{
    AimComponent _aim;
    GearComponent _gear;
    bool _attacking = false;
    float _attackTime = 0f;
    float _attackSpeed = .1f;
    HandsComponent _hands;
    GameObject _attackHand = null;

    public WeaponDefaultState(WeaponStatemachine statemachine, HandsComponent hands, GearComponent gear, AimComponent aim) : base(statemachine) {
        _aim = aim;
        _gear = gear;
        _hands = hands;
    }

    public bool GetTransitionCondition() {
        return _gear.Gear[GearItemSO.Slot.Primary].Item == null;
    }

    public void OnAwake() {
        _attackHand = Random.Range(0,2) == 0 ? _hands.PrimaryHand : _hands.SecondaryHand;
    }

    public void Tick() {
        if (!_attacking && Input.GetMouseButtonDown(0)) {
            _attacking = true;
        }

        if (_attacking) {
            if (_attackTime < _attackSpeed) {
                _attackTime += Time.smoothDeltaTime;
                var hp = (Vector2)_statemachine.transform.position + Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle) * 1f;
                _attackHand.transform.position = Vector2.Lerp(_attackHand.transform.position, hp, _attackTime);
                Debug.DrawLine(_statemachine.transform.position, hp, Color.magenta);
            }
            else {
                _attackTime = 0f;
                _attacking = false;
                _attackHand = Random.Range(0,2) == 0 ? _hands.PrimaryHand : _hands.SecondaryHand;
            }
            return;
        }

        else {
            var lookY = Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle);
            var offset = new Vector2(_statemachine.transform.position.x, _statemachine.transform.position.y - .5f);

            // this math just makes it look more like real hands but im calculating a offset because our characters sprite sits a bit lower, then im adding the hand pivot to it and then adding the look angle to that pivot times our hand distance
            // (offset + (handpos + lookangle) * distance
            _hands.PrimaryHand.transform.position = Vector2.Lerp(_hands.PrimaryHand.transform.position, offset + (_hands.PrimaryHandPosition + lookY) * _hands.HandDistance , _hands.DampSpeed);
            _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, offset + (_hands.SecondaryHandPosition + lookY) * _hands.HandDistance,_hands.DampSpeed);
        }
    }

    public void TransitionEnter() {
    }

    public void TransitionExit() {
    }
}