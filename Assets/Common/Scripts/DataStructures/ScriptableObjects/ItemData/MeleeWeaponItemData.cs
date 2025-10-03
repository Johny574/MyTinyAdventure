using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponItemData", menuName = "Items/Items/MeleeWeaponItemData", order = 1)]
public class MeleeWeaponItemData : WeaponItemData {
    [field: SerializeField] public float Reach = 5f;
    [field: SerializeField] public float Angle = 50f;
    [field: SerializeField] public AnimatorOverrideController Slash;
}