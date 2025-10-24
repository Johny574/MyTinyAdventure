using UnityEngine;

public class FlipBehaviour : MonoBehaviour
{
    public FlipComponent Flip { get; set; }
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] bool Invert = false;
    void Awake()
    {
        Flip = new(this, _renderer, Invert);
    }

    void Start() {
        var gear = GetComponent<GearBehaviour>();
        Flip.Initilize(gear.Gear);
    }
}