using System.Collections.Generic;
using UnityEngine;

public class WeaponDefaultState : StatemachineState<WeaponStatemachine, string>, IStatemachineState
{
    AimComponent _aim;
    GearComponent _gear;
    bool _attacking = false;
    float _attackTime = 0f;
    float _attackSpeed = .05f;
    HandsComponent _hands;
    GameObject _attackHand = null;
    bool _attacked = false;
    LayerMask _enemyLayer;
    StatpointsBehaviour _stats;
    float _reach = 2f;
    AudioSource _swingAudio;

    public WeaponDefaultState(WeaponStatemachine statemachine, HandsComponent hands, GearComponent gear, AimComponent aim, LayerMask enemyLayer, StatpointsBehaviour stats, AudioSource swingAudio) : base(statemachine) {
        _aim = aim;
        _gear = gear;
        _hands = hands;
        _enemyLayer = enemyLayer;
        _stats = stats;
        _swingAudio = swingAudio;
    }

    public bool GetTransitionCondition() {
        return _gear.Gear[GearItemSO.Slot.Primary].Item == null;
    }

    public void OnAwake() {
        if (_gear.Gear[GearItemSO.Slot.Secondary].Item == null)
        {
            _attackHand = _hands.PrimaryHand;
            return;
        }
        _attackHand = Random.Range(0,2) == 0 ? _hands.PrimaryHand : _hands.SecondaryHand;
    }

    public void Tick() {
        if (!_attacking && Input.GetMouseButtonDown(0) && _statemachine.CanAttack) {
            _attacking = true;
        }

        if (_attacking) {
            if (!_attacked) {
                _attacked = true;
                _swingAudio.Play();
                RaycastHit2D hit = Physics2D.Raycast(_statemachine.transform.position, Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle), _reach, _enemyLayer);

                if (hit)
                {
                    EntityStatemachine entity = hit.collider.gameObject.GetComponent<EntityStatemachine>();
                    entity.TakeDamage(_statemachine.transform.position, _stats.Stats);
                }
            }

            if (_attackTime < _attackSpeed) {
                _attackTime += Time.smoothDeltaTime;
                var hp = (Vector2)_statemachine.transform.position + Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle) * _reach;
                _attackHand.transform.position = Vector2.Lerp(_attackHand.transform.position, hp, _attackTime);
                Debug.DrawLine(_statemachine.transform.position, hp, Color.magenta);
            }
            else {
                _attackTime = 0f;
                _attacking = false;
                if (_gear.Gear[GearItemSO.Slot.Secondary].Item == null)
                    _attackHand = Random.Range(0,2) == 0 ? _hands.PrimaryHand : _hands.SecondaryHand;
            }
            return;
        }

        else {
            if (_attacked)
                _attacked = false;

            var lookY = Rotation2D.GetPointOnCircle(_statemachine.transform.position, _aim.LookAngle);
            var offset = new Vector2(_statemachine.transform.position.x, _statemachine.transform.position.y - .5f);

            // this math just makes it look more like real hands but im calculating a offset because our characters sprite sits a bit lower, then im adding the hand pivot to it and then adding the look angle to that pivot times our hand distance
            // (offset + (handpos + lookangle) * distance
            _hands.PrimaryHand.transform.position = Vector2.Lerp(_hands.PrimaryHand.transform.position, offset + (_hands.PrimaryHandPosition + lookY) * _hands.HandDistance , _hands.DampSpeed);

            if (_gear.Gear[GearItemSO.Slot.Secondary].Item != null)
                return;

            _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, offset + (_hands.SecondaryHandPosition + lookY) * _hands.HandDistance,_hands.DampSpeed);
        }
    }

    public void TransitionEnter() {
    }

    public void TransitionExit() {
    }
}