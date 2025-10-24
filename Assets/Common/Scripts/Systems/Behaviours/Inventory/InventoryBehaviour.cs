


using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(GearBehaviour))]
[RequireComponent(typeof(ExperienceBehaviour))]
public class InventoryBehaviour : MonoBehaviour
{
    public InventoryComponent Inventory { get; set; }
    public ItemStack[] Items = new ItemStack[16];
    HealthComponent _health;
    GearComponent  _gear;
    ExperienceComponent _xp;

    void Awake() {
        Inventory = new InventoryComponent(this, Items);
    }
    
    void Start() {
        _health = GetComponent<HealthBehaviour>().Health;
        _gear = GetComponent<GearBehaviour>().Gear;
        _xp = GetComponent<ExperienceBehaviour>().Experience;
        
        _health.Death += Inventory.OnDeath;
        _gear.Equiped += (item) => Inventory.Remove(new ItemStack(item, 1));
        _gear.Unequiped += (item) => Inventory.Add(new ItemStack(item, 1));
        Inventory.Initilize( Items);
    }
}