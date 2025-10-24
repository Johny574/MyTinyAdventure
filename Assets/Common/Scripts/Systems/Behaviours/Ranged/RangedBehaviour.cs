
using UnityEngine;


[RequireComponent(typeof(StatpointsBehaviour))]
public class RangedBehaviour : MonoBehaviour
{
    public RangedComponent Ranged { get; set; }
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] ProjectileSO _projectile;

    void Awake() {
        Ranged = new(this, _targetLayer, _projectile);
    }
    void Start() {
        GearBehaviour gear;
        if (TryGetComponent(out gear)) {
            gear.Gear.Equiped += ItemEquiped;
            ItemEquiped(gear.Gear.Gear[GearItemSO.Slot.Primary].Item);
        }
        var stats = GetComponent<StatpointsBehaviour>().Stats;
        Ranged.Initilize(stats);
    }
    void ItemEquiped(GearItemSO item) {
        if (item?.Target != GearItemSO.Slot.Primary || item?.GetType() != typeof(RangedWeaponItemData)) {
            return;
        }

        Ranged.ChangeProjectile(((RangedWeaponItemData)item).Projectile);
    }
}