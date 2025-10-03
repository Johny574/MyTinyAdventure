using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponItemData", menuName = "Items/Items/RangedWeaponItemData", order = 1)]
public class RangedWeaponItemData : WeaponItemData {
    [field:SerializeField] public ProjectileSO Projectile { get; set; }
    [field:SerializeField] public float Duration { get; set; }
   
}