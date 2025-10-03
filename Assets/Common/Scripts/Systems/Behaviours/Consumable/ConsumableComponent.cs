using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ConsumableComponent : Component, ISerializedComponent<ConsumableData[]>
{
    public Action<Consumable[]> Updated;
    public Consumable[] Consumables { get; private set; } 
    public ConsumableComponent(ConsumableBehaviour behaviour) : base(behaviour) {
        Consumables = new Consumable[2];
    }

    public void Load(ConsumableData[] save) {
        Consumables = new Consumable[2];
        for (int i = 0; i < save.Length; i++) {
            if (save[i] != null) {
                int index = i;
                ConsumableData item = save[index]; // keep this reference in memory

                UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<ItemSO> ItemSO = Addressables.LoadAssetAsync<ItemSO>(new AssetReference(item.GUID));
                ItemSO.Completed += (itemso) => {
                    if (itemso.Status.Equals(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed))
                        throw new System.Exception($"Failed to load asset {item.GUID}");

                    Consumables[index] = new Consumable(new ItemStack(itemso.Result, item.Counter), item.Timer);
                    Updated?.Invoke(Consumables);
                };
            }
        }
    }

    public ConsumableData[] Save() => Consumables.Select(x => x.Equals(null) ? null : new ConsumableData(x.Stack.Item.GUID, x.Stack.Count, x.Timer)).ToArray();

    public void Add(int slot, ItemStack consumable) {
        if (consumable.Item.GetType() != typeof(ConsumableSO))
            return;

        Consumables[slot] = new Consumable(consumable);
        Updated?.Invoke(Consumables);
    }

    public void Remove(int slot) {
        Consumables[slot] = null;
        Updated?.Invoke(Consumables);
    }

    public void Update() {
        for (int i = 0; i < Consumables.Length; i++) {
            if (Consumables[i] != null) {
                if (Consumables[i].Stack.Item != null)
                    Consumables[i].Tick();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
            Consumables[0].Consume(this);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            Consumables[1].Consume(this);
    }
}