
using UnityEngine;

public class SecondaryWeaponBehaviour : MonoBehaviour
{
    public SecondaryWeaponComponent SecondaryWeaponComponent { get; set; }
    [SerializeField] LayerMask _enemies;
    GearComponent _gear;

    void Awake()
    {
        SecondaryWeaponComponent = new SecondaryWeaponComponent(this);
    }

    void Start()
    {
        _gear = GetComponent<GearBehaviour>().Gear;
        SecondaryWeaponComponent.Initilize(_gear, GetComponent<HandsBehaviour>().Hands, GetComponent<FlipBehaviour>().Flip, GetComponent<StatpointsBehaviour>().Stats, GetComponent<AimBehaviour>().Aim);
    }

    void Update()
    {
        SecondaryWeaponComponent.Tick();

        if (Input.GetMouseButtonDown(1) && _gear.Gear[GearItemSO.Slot.Secondary].Item != null)
            SecondaryWeaponComponent.Attacking = true;
    }
}