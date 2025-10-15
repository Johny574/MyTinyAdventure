using UnityEngine;

public class FlipBehaviour : MonoBehaviour
{
    public FlipComponent Flip { get; set; }
    [SerializeField] SpriteRenderer _renderer;
    void Awake()
    {
        Flip = new(this, _renderer);
    }

    void Start() {
        var gear = GetComponent<GearBehaviour>();
        Flip.Initilize(gear.Gear);
    }
}