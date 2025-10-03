



using UnityEngine;


[RequireComponent(typeof(FlipBehaviour))]
public class HandsBehaviour : MonoBehaviour
{
    [SerializeField] Vector2 _primaryWeaponPivot = new Vector2(-.25f, -.5f);
    [Range(0, 1)]
    [SerializeField] float _dampSpeed = 0.1f;
    public HandsComponent Hands { get; set; }

    [SerializeField] GameObject _primaryHand, _secondaryHand;

    void Awake() {
        Hands = new(this, _primaryHand, _secondaryHand, _primaryWeaponPivot, _dampSpeed);
    }

    void Start() {
        Hands.Initilize(GetComponent<FlipBehaviour>().Flip);
    }
}