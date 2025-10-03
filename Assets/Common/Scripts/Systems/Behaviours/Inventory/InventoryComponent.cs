using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InventoryComponent : Component, ISerializedComponent<ItemStackData[]> {
    public ItemStack[] Inventory = new ItemStack[16];
    public Action<ItemStack, ItemStack[]> Added, Removed;

    HealthComponent _health;
    GearComponent  _gear;
    ExperienceComponent _xp;

    public InventoryComponent(MonoBehaviour behaviour, ItemStack[] inventory) : base(behaviour) {
        Inventory = inventory;
    }

    public void Initilize() {
        _health = Behaviour.GetComponent<HealthBehaviour>().Health;
        _gear = Behaviour.GetComponent<GearBehaviour>().Gear;
        _xp = Behaviour.GetComponent<ExperienceBehaviour>().Experience;

        Inventory = Enumerable.Repeat(new ItemStack(null, 0), _xp.CurrentLevel.InventorySlots).ToArray();
        _health.Death += OnDeath;

        _gear.Equiped += (item) => Remove(new ItemStack(item, 1));
        _gear.Unequiped += (item) => Add(new ItemStack(item, 1));
    }

    public void Add(ItemStack stack) {
        // set total
        int c = stack.Count;
        int newCount;

        // grab all slots that aren't fully filled eg. rope - count 8/12.
    
        List<ItemStack> itemSlots = Inventory.Where(x => x.Item != null).Where(x => x.Item == stack.Item).Where(x => x.Count < x.Item.Limit).ToList();

        ItemStack handle;
        for (int i = 0; i < itemSlots.Count(); i++) {
            // return if we dont need to add anymore.
            if (c <= 0) break;

            handle = itemSlots[i];
            var item = handle.Item;
            newCount = handle.Count + c;

            // set count.
            if (newCount > item.Limit) {
                newCount = item.Limit;
                c = handle.Count + c - item.Limit;
            }
            else {
                c = 0;
            }

            // update inventory slot.
            int index = Array.IndexOf(Inventory, handle);
            Inventory[index] = new ItemStack(stack.Item, newCount);
        }

        // grab all slots that are empty.
        var items = Inventory.Where(x => x.Item != null);
        int itemCount = Inventory.Length - items.Count();

        for (int i = items.Count(); i < items.Count() + itemCount; i++) {
            // return if we don't need to add anymore.
            if (c <= 0) break;

            // calculate the size of the new stack.
            var item = stack.Item;
            newCount = c - item.Limit <= 0 ? c : item.Limit;
            // set new stack.
            Inventory[i] = new ItemStack(stack.Item, newCount);

            // update total.
            c -= newCount;
        }

        // inventory full.
        if (c > 0) throw new InventoryFullException(c);
        
        // sort inventory
        Inventory = Inventory.OrderByDescending(x => x.Item != null).ToArray();
        Added?.Invoke(stack, Inventory);
    }

    public void Remove(ItemStack stack) {
        // set total.
        int c = stack.Count;

        // loop over inventory
        Inventory = Inventory.OrderBy(x => x.Item != null).ToArray();
        List<ItemStack> slots = Inventory.Where(x => x.Item != null).Where(x => x.Item == stack.Item).ToList();
        for (int i = 0; i < slots.Count; i++) {
            // return if we dont need to subtract from the stack anymore.
            if (c <= 0) break;

            // clear slot
            if (slots[i].Count - c <= 0) {
                c = Mathf.Abs(slots[i].Count - c);
                Inventory[Array.IndexOf(Inventory, slots[i])] = new ItemStack(null, 0);
            }
            // update slot
            else {
                Inventory[Array.IndexOf(Inventory, slots[i])].Update(0 - c);
                c = 0;
            }
        }

        // sort inventory
        Inventory = Inventory.OrderByDescending(x => x.Item != null).ToArray();
        Removed?.Invoke(stack, Inventory);
    }

    public void Resize(int newSize) {
        if (newSize > Inventory.Length) {
            ItemStack[] newInventory = new ItemStack[newSize];
            Array.Copy(Inventory, newInventory, Inventory.Count());
            Inventory = newInventory;
        }
    }

    public void OnDeath() {
        for (int i = 0; i < Inventory.Length; i++) {
            // new InventoryCommands.DropCommand(Inventory[i], Entity, Entity.Component<Transform>().position).Execute();
        }
    }

    public ItemStackData[] Save() => Inventory.Where(x => x.Item != null).Select(x => new ItemStackData(x.Item.GUID, x.Count)).ToArray();

    public void Load(ItemStackData[] save) {
        Inventory = Enumerable.Repeat(new ItemStack(null, 0), _xp.CurrentLevel.InventorySlots).ToArray();
        for (int i = 0; i < save.Length; i++) {
            ItemStackData item = save[i]; // keep this reference in memory

            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<ItemSO> ItemSO = Addressables.LoadAssetAsync<ItemSO>(new AssetReference(item.GUID));
            ItemSO.Completed += (itemso) => {
                if (itemso.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed) {
                    throw new System.Exception($"Failed to load asset {item.GUID}");
                }
                var stack = new ItemStack(itemso.Result, item.Count);
                Inventory[i] = stack;
                Inventory = Inventory.OrderByDescending(x => x.Item != null).ToArray();
                // Added?.Invoke(stack, Inventory);
            };
        }
    }
}

public class InventoryFullException : Exception {
    public int Remainder;

    public InventoryFullException(int remainder) {
        Remainder = remainder;
    }
}