using UnityEngine;

public class HandsComponent : Component 
{
    #region  General
    public GameObject PrimaryHand, SecondaryHand;
    public float DampSpeed { get; set; } = 1f;
    public float HandDistance = .2f;
    FlipComponent _flip;
    #endregion

    #region Pivots
    Vector2 _primaryHandPivot;
    Vector2 _secondaryWeaponPivot;
    #endregion

    public HandsComponent(HandsBehaviour behaviour,  GameObject primaryHand, GameObject secondaryHand,  Vector2 primaryHandPivot, Vector2 secondaryHandPivot, float dampspeed) : base(behaviour) {
        DampSpeed = dampspeed;
        _primaryHandPivot = primaryHandPivot;
        _secondaryWeaponPivot = secondaryHandPivot;
        PrimaryHand = primaryHand;
        SecondaryHand = secondaryHand;
    }

    public void Initilize(FlipComponent flip) {
        _flip = flip;
    }

    // todo this can be fixed
    public Vector2 PrimaryHandPosition           => _flip.Flipped ? new Vector2(Mathf.Abs(_primaryHandPivot.x), _primaryHandPivot.y) : _primaryHandPivot;
    public Vector2 SecondaryHandPosition    => _flip.Flipped ? _secondaryWeaponPivot : new Vector2(Mathf.Abs(_secondaryWeaponPivot.x), _secondaryWeaponPivot.y);
}