


using UnityEngine;

public class SecondaryWeaponComponent : Component
{
    GearComponent _gear;
    HandsComponent _hands;
    FlipComponent _flip;
    StatpointsComponent _stats;
    AimComponent _aim;

    #region Logic
    public bool Attacking = false;
    float _attackTime = 0f;
    LayerMask _enemies;
    float _reach = .5f, _attackSpeed = .25f;
    #endregion

    public SecondaryWeaponComponent(UnityEngine.MonoBehaviour behaviour) : base(behaviour)
    {
    }

    public void Tick()
    {
          if (Attacking)
            Attack();
        else
            Relax();
    }

    public void Initilize(GearComponent gear, HandsComponent hands, FlipComponent flip, StatpointsComponent stats, AimComponent aim)
    {
        _gear = gear;
        _hands = hands;
        _flip = flip;
        _stats = stats;
        _aim = aim;
    }
    
     void Attack() {
        if (_attackTime < _attackSpeed) {
            _attackTime += Time.smoothDeltaTime;
            var hp = (Vector2)Behaviour.transform.position + Rotation2D.GetPointOnCircle(Behaviour.transform.position, _aim.LookAngle) * _reach;
            _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, hp, _attackTime);
            _gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position, hp, _attackTime);
        }
        else {
            _attackTime = 0f;
            Attacking = false;
        }
    }

    void Relax() {
        if (_gear.Gear[GearItemSO.Slot.Secondary].Item == null)
            return;

        _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, (Vector2)Behaviour.transform.position + _hands.SecondaryHandPosition, _hands.DampSpeed);
        _gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position, (Vector2)Behaviour.transform.position + _hands.SecondaryHandPosition, _hands.DampSpeed);
    }

}