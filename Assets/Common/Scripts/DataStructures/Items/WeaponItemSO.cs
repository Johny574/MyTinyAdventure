using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/Items/WeaponItemData", order = 1)]
public class WeaponItemSO : GearItemSO {
    public Type DamageType;
    public SkillClass[] SkillClasses;
    public float AttackSpeed = 1f;
    public Sprite HotbarIcon;
    public enum Type {
        Magic,
        Ranged,
        Melee
    }
}

[Serializable]
public class SkillClass { 
    public SkillSO[] Skills;
}