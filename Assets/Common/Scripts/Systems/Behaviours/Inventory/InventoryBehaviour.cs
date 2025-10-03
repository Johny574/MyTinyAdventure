


using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(GearBehaviour))]
[RequireComponent(typeof(ExperienceBehaviour))]
public class InventoryBehaviour : MonoBehaviour
{
    public InventoryComponent Inventory { get; set; }
    public ItemStack[] Items = new ItemStack[16];

    void Awake() {
        Inventory = new InventoryComponent(this, Items);
    }
    
    void Start() {
        Inventory.Initilize();
    }
}