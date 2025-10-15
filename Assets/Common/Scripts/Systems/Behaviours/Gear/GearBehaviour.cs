using UnityEngine;

public class GearBehaviour : MonoBehaviour
{
    public GearComponent Gear { get; set; }

    [SerializeField]
    GearSlots _gear = new() {
        { GearItemSO.Slot.Primary , new Gearslot(null, null, null)},
        { GearItemSO.Slot.Secondary , new Gearslot(null, null, null)}
    };
    void Awake() {
        var animator = GetComponent<Animator>();
        Gear = new(this, _gear, animator);
    }
    void Start() {
        Gear.Initilize(GetComponent<HealthBehaviour>().Health);
    }
}