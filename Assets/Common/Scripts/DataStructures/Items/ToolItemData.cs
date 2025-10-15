using UnityEngine;

[CreateAssetMenu(fileName = "ToolItemData", menuName = "Items/Items/ToolItemData", order = 1)]
public class ToolItemData : ItemSO {
    public Type _Type;
    public enum Type {
        Hammer,
        Axe
    }
}