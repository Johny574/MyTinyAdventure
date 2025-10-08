using UnityEngine;

public class MeleeComponent : Component
{
    TrailRenderer _trailRenderer;
    GearComponent _gear;
    HandsComponent _hands;
    FlipComponent _flip;
    StatpointsComponent _stats;

    #region Arc
    float _swingArcDegrees = 135;
    float _swingStartAngle, _swingStopAngle, _swingAngle;
    #endregion

    #region Audio
    AudioSource _whooshAudio;
    #endregion

    #region Random
    float _targetStartAngle, _targetEndAngle;
    bool _setRandomSwing = false;
    #endregion

    #region Logic
    public bool Attacking = false;
    float _attackTime = 0f;
    LayerMask _enemies;
    #endregion

    public MeleeComponent(MeleeBehaviour behaviour, TrailRenderer trailRenderer, float meleeswingdegrees, LayerMask enemies, AudioSource whooshaudio) : base(behaviour) {
        _swingArcDegrees = meleeswingdegrees;
        _trailRenderer = trailRenderer;
        _enemies = enemies;
        _whooshAudio = whooshaudio;
    }

    public void Initilize(GearComponent gear, HandsComponent hands, FlipComponent flip, StatpointsComponent stats) {
        _gear = gear;
        _hands = hands;
        _flip = flip;
        _stats = stats;
    }

    public void Attack(Vector2 direction) {
        if (Attacking)
            return;

        float angle = Rotation2D.LookAngle(direction, 0);
        if (angle < 0) angle += 360;

        _swingStartAngle = !_flip.Flipped ? angle - _swingArcDegrees : angle + _swingArcDegrees;
        _swingStopAngle = !_flip.Flipped ? angle + _swingArcDegrees : angle - _swingArcDegrees;

        Attacking = true;
    }

    public void Tick() {
        if (Attacking)
            Attack();
        else
            Relax();
    }

    void Attack() {
        var primaryslot = _gear.Gear[GearItemSO.Slot.Primary];

        if (!primaryslot.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            primaryslot.Animator.CrossFade("Attack", 0f);

        if (!_trailRenderer.emitting)
            _trailRenderer.emitting = true;

        if (!_setRandomSwing) {
            _whooshAudio.Play();
            _targetStartAngle = UnityEngine.Random.Range(0, 2) > 0 ? _swingStartAngle : _swingStopAngle;
            _targetEndAngle = _targetStartAngle == _swingStartAngle ? _swingStopAngle : _swingStartAngle;
            _setRandomSwing = true;
        }

        if (_attackTime + Time.smoothDeltaTime < primaryslot.Animator.GetCurrentAnimatorStateInfo(0).length) {
            _swingAngle = Mathf.Lerp(_targetStartAngle, _targetEndAngle, _attackTime / primaryslot.Animator.GetCurrentAnimatorStateInfo(0).length);

            Vector3 swingpos = (Vector2)Behaviour.transform.position + Rotation2D.GetPointOnCircle(Behaviour.transform.position, _swingAngle);
            Vector3 swingDirection = (swingpos - Behaviour.transform.position).normalized;

            primaryslot.Object.transform.position = swingpos;
            primaryslot.Object.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _swingAngle - 90));

            _hands.PrimaryHand.transform.position = swingpos;
            _hands.PrimaryHand.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _swingAngle - 90));
            _hands.SecondaryHand.transform.position = swingpos;
            _hands.SecondaryHand.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _swingAngle - 90));

            RaycastHit2D hit = Physics2D.Raycast(Behaviour.transform.position, swingDirection, ((MeleeWeaponItemData)_gear.Gear[GearItemSO.Slot.Primary].Item).Reach, _enemies);

            if (hit) {
                EntityStatemachine entity = hit.collider.gameObject.GetComponent<EntityStatemachine>();
                entity.TakeDamage(Behaviour.transform.position, _stats);
            }
            _attackTime += Time.smoothDeltaTime;
        }

        else {
            Attacking = false;
            _attackTime = 0f;
        }
    }

    void Relax() {
        if (_setRandomSwing)
            _setRandomSwing = false;

        if (_trailRenderer.emitting)
            _trailRenderer.emitting = false;

        _gear.Gear[GearItemSO.Slot.Primary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Primary].Object.transform.position, (Vector2)Behaviour.transform.position + _hands.PrimaryWeaponPos, _hands.DampSpeed);
        _gear.Gear[GearItemSO.Slot.Primary].Object.transform.rotation = Quaternion.Lerp(_gear.Gear[GearItemSO.Slot.Primary].Object.transform.rotation, Quaternion.Euler(Vector3.zero), _hands.DampSpeed);
        _hands.PrimaryHand.transform.position = Vector2.Lerp(_hands.PrimaryHand.transform.position, (Vector2)Behaviour.transform.position + _hands.PrimaryHandPosition, _hands.DampSpeed);

        if (_gear.Gear[GearItemSO.Slot.Secondary].Item == null)
            _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, (Vector2)Behaviour.transform.position + new Vector2(_hands.PrimaryWeaponPos.x, _hands.PrimaryHandPosition.y - .2f), _hands.DampSpeed);
        else
            _hands.SecondaryHand.transform.position = Vector2.Lerp(_hands.SecondaryHand.transform.position, (Vector2)Behaviour.transform.position + _hands.SecondaryHandPosition, _hands.DampSpeed);

        _gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position = Vector2.Lerp(_gear.Gear[GearItemSO.Slot.Secondary].Object.transform.position, (Vector2)Behaviour.transform.position + _hands.SecondaryWeaponPos, _hands.DampSpeed);
        _swingAngle = _swingStartAngle;
    }
}