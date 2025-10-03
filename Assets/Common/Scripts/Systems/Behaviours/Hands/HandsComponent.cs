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
    Vector2 _primaryWeaponPivot;
    #endregion

    public HandsComponent(HandsBehaviour behaviour,  GameObject primaryHand, GameObject secondaryHand,  Vector2 primaryHandPivot, float dampspeed) : base(behaviour) {
        DampSpeed = dampspeed;
        _primaryWeaponPivot = primaryHandPivot;
        PrimaryHand = primaryHand;
        SecondaryHand = secondaryHand;
    }

    public void Initilize(FlipComponent flip) {
        _flip = flip;
    }

    public Vector2 PrimaryWeaponPos         => _flip.Flipped ? new Vector2(Mathf.Abs(_primaryWeaponPivot.x), _primaryWeaponPivot.y) : _primaryWeaponPivot;
    public Vector2 SecondaryWeaponPos       => _flip.Flipped ? _primaryWeaponPivot : new Vector2(Mathf.Abs(_primaryWeaponPivot.x), _primaryWeaponPivot.y);
    public Vector2 PrimaryHandPosition           => _flip.Flipped ? new Vector2(Mathf.Abs(_primaryWeaponPivot.x), _primaryWeaponPivot.y) : _primaryWeaponPivot;
    public Vector2 SecondaryHandPosition    => _flip.Flipped ? _primaryWeaponPivot : new Vector2(Mathf.Abs(_primaryWeaponPivot.x), _primaryWeaponPivot.y);
}