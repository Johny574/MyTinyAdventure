using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour {
    // public static PlayerEntityService Instance { get; set; }
    // public PlayerEntityService(Dictionary<string, IService> services, Dictionary<string, Component> components, EntityServiceBehaviour behaviour) : base(services, components, behaviour) {
    //     Instance = this;
    // }
    
    // public PlayerSave Save() => new
    // (
        // Service<HealthService>().Get<Counter>(),
        // Service<InventoryService>().Get<ItemStack<int>[]>(),
        // Service<ExperienceService>().Get<KeyValuePair<int, Counter>>(),
        // Service<ManaService>().Get<Counter>(),
        // Service<CurrencyService>().Get<int>(),
        // Service<BuffService>().Get<List<ItemStack<int>>>(),
        // Service<StatService>().Get<Dictionary<string, List<ItemStack<string>>>>(),
        // Service<GearService>().Get<Dictionary<string, int>>(),
        // Service<JournalService>().Get<Dictionary<int, int>>(),
        // Service<SkillService>().Get<int[]>(),
        // Service<ConsumableService>().Get<ItemStack<int>[]>(),
        // Service<LocationService>().Get<KeyValuePair<int, Counter>>()
    // );

    public void Load(PlayerSaveData save) { 
        // Service<HealthService>().Set(save.Health);
        // Service<InventoryService>().Set(save.Inventory);
        // Service<ExperienceService>().Set(save.Experience);
        // Service<ManaService>().Set(save.Mana);
        // Service<CurrencyService>().Set(save.Currency);
        // Service<BuffService>().Set(save.Buffs);
        // Service<StatService>().Set(save.Stats);
        // Service<GearService>().Set(save.Gear);
        // Service<JournalService>().Set(save.Journal);
        // Service<SkillService>().Set(save.Skills);
        // Service<ConsumableService>().Set(save.Consumables);
        // Service<LocationService>().Set(save.Location);
    }
}