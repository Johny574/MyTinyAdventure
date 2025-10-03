using UnityEngine;

[CreateAssetMenu(fileName = "GearItem", menuName = "Items/Items/GearItem", order = 1)]
public class GearItemSO : ItemSO {
    public Slot Target;
    public StatPoints Stats;
    public enum Slot
    {
        Primary,
        Secondary,
        Necklace,
        Boots,
        Helmet,
        Ring1,
        Ring2,
        Torso,
        Pants,
        Tome
    } 
}