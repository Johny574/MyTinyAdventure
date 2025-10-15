using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponItemData", menuName = "Items/Items/MeleeWeaponItemData", order = 1)]
public class MeleeWeaponItemData : WeaponItemSO {
    [field: SerializeField] public float Reach = 5f;
    [field: SerializeField] public float Angle = 50f;
}